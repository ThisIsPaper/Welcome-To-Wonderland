
namespace Wonderland.Logic.Controllers.Surface
{
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.SagePay;

    public class DonateSurfaceController : SurfaceController
    {
        public ActionResult RenderDonateForm()
        {
            DonateForm donateForm = new DonateForm();
            
            // set hidden field with party guid
            donateForm.PartyGuid = ((Donate)this.CurrentPage).PartyHost.PartyGuid;

            PartyHost partyHost = this.Members.GetPartyHost(donateForm.PartyGuid);
            IPartier partier = this.Members.GetCurrentPartier();

            // set the default amount to the party host's suggested donation (only if user isn't the party host)            
            if (partier is PartyHost && partier.Id == partyHost.Id)
            {
                donateForm.Amount = 0;
            }
            else
            {
                donateForm.Amount = partyHost.SuggestedDonation;
            }

            if (partier != null)
            {
                donateForm.FirstName = partier.FirstName;
                donateForm.LastName = partier.LastName;

                donateForm.Address1 = partier.BillingAddress.Address1;
                donateForm.Address2 = partier.BillingAddress.Address2;
                donateForm.TownCity = partier.BillingAddress.TownCity;
                donateForm.Postcode = partier.BillingAddress.Postcode;
            }

            return this.PartialView("Donate/Forms/DonateForm", donateForm);
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

            if (this.Members.IsLoggedInPartier())
            {
                memberId = this.Members.GetCurrentMemberId();
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
                                            PaymentJourney = PaymentJourney.Donate,
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

            this.ViewData["errorMessage"] = transactionRegistrationResponse.StatusDetail;

            return this.View("Donate/Failed", this.CurrentPage);
        }
    }
}
