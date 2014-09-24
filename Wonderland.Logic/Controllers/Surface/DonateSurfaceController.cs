
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.SagePay;
    using Wonderland.Logic.Extensions;

    public class DonateSurfaceController : SurfaceController
    {
        /// <summary>
        /// Handles inbound links to the donate page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NavigateToDonateUrl(Guid partyGuid)
        {
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + Donate.Alias).Url + "?partyGuid=" + partyGuid.ToString());
        }

        public ActionResult RenderDonateForm()
        {
            DonateForm donateForm = new DonateForm();

            // set hidden field with party guid
            donateForm.PartyGuid = ((Donate)this.CurrentPage).PartyHost.PartyGuid;

            if (this.Members.IsLoggedIn())
            {
                IPartier partier = (IPartier)this.Members.GetCurrentMember();

                donateForm.Address1 = partier.BillingAddress.Address1;
                donateForm.Address2 = partier.BillingAddress.Address2;
                donateForm.TownCity = partier.BillingAddress.TownCity;
                donateForm.Postcode = partier.BillingAddress.Postcode;
            }

            return this.PartialView("DonateForm", donateForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleDonateForm(DonateForm donateForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            int? memberId = null;

            if (this.Members.IsLoggedIn())
            {
                memberId = this.Members.GetCurrentMemberId();

                // TODO: set the billing address for the current member ? (default billing = last used)
                Address address = new Address()
                                    {
                                        Address1 = donateForm.Address1,
                                        Address2 = donateForm.Address2,
                                        TownCity = donateForm.TownCity,
                                        Postcode = donateForm.Postcode
                                    };

                ((IPartier)this.Members.GetCurrentMember()).BillingAddress = address;                
            }

            DonationRow donationRow = new DonationRow()
                                        {
                                            PartyGuid = donateForm.PartyGuid,
                                            Amount = donateForm.Amount,
                                            GiftAid = donateForm.AllowGiftAid,
                                            MemberId = memberId, 
                                            FirstName = donateForm.FirstName,
                                            LastName = donateForm.LastName,
                                            Address1 = donateForm.Address1,
                                            Address2 = donateForm.Address2,
                                            TownCity = donateForm.TownCity,
                                            Postcode = donateForm.Postcode,
                                            Timestamp = DateTime.Now,
                                            Success = false
                                        };

            // insert new record
            this.DatabaseContext.Database.Insert(donationRow);

            // build new obj containing data for sage pay
            TransactionRegistrationRequest transactionRegistrationRequest = new TransactionRegistrationRequest(donationRow);

            // send to sage pay and get respone
            TransactionRegistrationResponse transactionRegistrationResponse = TransactionRegistration.Send(transactionRegistrationRequest);

            // based on response, we redirect the user to...
            if (transactionRegistrationResponse.Status ==TransactionRegistrationStatus.OK)
            {
                // update database
                donationRow.VPSTxId = transactionRegistrationResponse.VPSTxId;
                donationRow.SecurityKey = transactionRegistrationResponse.SecurityKey;

                this.DatabaseContext.Database.Update(donationRow);

                return this.Redirect(transactionRegistrationResponse.NextURL);
            }
            else
            {
                // delete row ? (as transaction will never happen)
            }

            return this.CurrentUmbracoPage(); // TODO: return a view indicating failure
        }
    }
}
