
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.SagePay;
    using Wonderland.Logic.DotMailer;

    public class RegisterGuestSurfaceController : SurfaceController
    {
        /// <summary>
        /// returns the partial for the guest registration form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RenderRegisterGuestForm()
        {
            // get party guid from the querystring
            return this.PartialView("RegisterGuest/Forms/RegisterGuestForm", new RegisterGuestForm(Guid.Parse(this.Request.QueryString["partyGuid"])));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleRegisterGuestForm(RegisterGuestForm registerGuestForm)
        {
            if (!this.ModelState.IsValid)
            {                
                return this.CurrentUmbracoPage();
            }

            // no helper method on this.Members to register a user with a given memberType, so calling provider directly
            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = (UmbracoMembershipProviderBase)Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName];

            MembershipCreateStatus membershipCreateStatus;

            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                                PartyGuest.Alias,                               // member type alias
                                                registerGuestForm.EmailAddress,                 // username
                                                registerGuestForm.Password,                     // password
                                                registerGuestForm.EmailAddress,                 // email
                                                null,                                           // forgotten password question
                                                null,                                           // forgotten password answer
                                                true,                                           // is approved 
                                                null,                                           // provider user key
                                                out membershipCreateStatus);

            if (membershipCreateStatus != MembershipCreateStatus.Success)
            {
                switch (membershipCreateStatus)
                {
                    case MembershipCreateStatus.DuplicateEmail:
                    case MembershipCreateStatus.DuplicateUserName:

                        this.ModelState.AddModelError("RegisterGuestValidation", "Email already registered");

                        break;
                }

                return this.CurrentUmbracoPage();
            }

            // cast from MembershipUser rather than use this.Members.GetCurrentMember() helper (which needs a round trip for the login)
            PartyGuest partyGuest = (PartyGuest)membershipUser;

            // update database with member and party guid (duplicated data, but never changes)
            this.DatabaseContext.Database.Insert(new MemberPartyRow(partyGuest.Id, registerGuestForm.PartyGuid));

            // (duplicate data) store party guid in cms cache
            partyGuest.PartyGuid = registerGuestForm.PartyGuid;

            // add member to DotMailer
            DotMailerService.AddContact((Contact)partyGuest);

            // send cookie
            FormsAuthentication.SetAuthCookie(partyGuest.Username, true);

            //return this.NavigateToRegisterGuestUrl(registerGuestForm.PartyGuid);
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + RegisterGuest.Alias).Url + "?partyGuid=" + registerGuestForm.PartyGuid.ToString());
        }

        [MemberAuthorize(AllowType=PartyGuest.Alias)]
        [ChildActionOnly]
        public ActionResult RenderRegisterGuestBillingForm()
        {
            RegisterGuestBillingForm registerGuestBillingForm = new RegisterGuestBillingForm();

            // set hidden field with party guid
            registerGuestBillingForm.PartyGuid = ((RegisterGuest)this.CurrentPage).PartyHost.PartyGuid;

            // set the default amount to the party host's suggested donation
            registerGuestBillingForm.Amount = this.Members.GetPartyHost(registerGuestBillingForm.PartyGuid).SuggestedDonation;

            return this.PartialView("RegisterGuest/Forms/RegisterGuestBillingForm", registerGuestBillingForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyGuest.Alias)]
        public ActionResult HandleRegisterGuestBillingForm(RegisterGuestBillingForm registerGuestBillingForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            if (!string.IsNullOrWhiteSpace(registerGuestBillingForm.Message))
            {
                // post message to party wall
                this.DatabaseContext.Database.Insert(new MessageRow()
                                                        {
                                                            MemberId = this.Members.GetCurrentMemberId(),
                                                            Text = registerGuestBillingForm.Message,
                                                            Image = null
                                                        });
            }

            DonationRow donationRow = new DonationRow()
            {
                PartyGuid = registerGuestBillingForm.PartyGuid,
                Amount = registerGuestBillingForm.Amount,
                GiftAid = registerGuestBillingForm.AllowGiftAid,
                MemberId = this.Members.GetCurrentMemberId(),
                FirstName = registerGuestBillingForm.FirstName,
                LastName = registerGuestBillingForm.LastName,
                Address1 = registerGuestBillingForm.Address1,
                Address2 = registerGuestBillingForm.Address2,
                TownCity = registerGuestBillingForm.TownCity,
                Postcode = registerGuestBillingForm.Postcode,
                PaymentJourney = PaymentJourney.RegisterGuest,
                Success = false
            };

            // insert new record
            this.DatabaseContext.Database.Insert(donationRow);

            // build new obj containing data for sage pay
            TransactionRegistrationRequest transactionRegistrationRequest = new TransactionRegistrationRequest(donationRow);

            // send to sage pay and get respone
            TransactionRegistrationResponse transactionRegistrationResponse = TransactionRegistration.Send(transactionRegistrationRequest);

            // based on response, we redirect the user to...
            if (transactionRegistrationResponse.Status == TransactionRegistrationStatus.OK)
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

            this.ViewData["errorMessage"] = transactionRegistrationResponse.StatusDetail;

            return this.View("RegisterGuest/Failed", this.CurrentPage);
        }
    }
}

