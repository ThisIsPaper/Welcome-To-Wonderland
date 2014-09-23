
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.SagePay;
    using Wonderland.Logic.Models.Entities;

    public class DonateSurfaceController : SurfaceController
    {
        /// <summary>
        /// Handles inbound links to the donate page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        //[MemberAuthorize]
        public ActionResult NavigateToDonateUrl(Guid partyGuid)
        {
            // TODO: if not logged in, then redirect to login page first ?

            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + Donate.Alias).Url + "?partyGuid=" + partyGuid.ToString());
        }

        [MemberAuthorize]
        public ActionResult RenderDonateForm()
        {
            // TODO: check member is logged in

            DonateForm donateForm = new DonateForm();

            //donateForm.FirstName = 

            return this.PartialView("DonateForm", donateForm);
        }

        [HttpPost]
        [MemberAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult HandleDonateForm(DonateForm donateForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            // TODO: insert into db here to get the vendorTxCode...

            int vendorTxCode = 10000;
            decimal amount = donateForm.Amount;
            bool allowGiftAid = donateForm.AllowGiftAid;
            string firstName = donateForm.FirstName;
            string lastName = donateForm.LastName;
            Address address = new Address()
                                    {
                                        Address1 = donateForm.Address1,
                                        Address2 = donateForm.Address2,
                                        TownCity = donateForm.TownCity,
                                        Postcode = donateForm.PostCode
                                    };

            // build data to send to sage pay in preparation for a transaction, and get the sage pay response
            TransactionRegistrationResponse transactionRegistrationResponse = TransactionRegistration.Send(new TransactionRegistrationRequest(vendorTxCode, amount, allowGiftAid, firstName, lastName, address));

            // based on response, we redirect the user to...

            return this.CurrentUmbracoPage();
        }
    }
}
