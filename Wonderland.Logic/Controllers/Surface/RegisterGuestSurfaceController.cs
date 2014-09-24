
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class RegisterGuestSurfaceController : SurfaceController
    {
        /// <summary>
        /// Handles inbound links to the register guest page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NavigateToRegisterGuestUrl(Guid partyGuid)
        {
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + RegisterGuest.Alias).Url + "?partyGuid=" + partyGuid.ToString());
        }

        /// <summary>
        /// returns the partial for the guest registration form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly]
        public ActionResult RenderRegisterGuestForm()
        {
            // get party guid from the querystring
            return this.PartialView("RegisterGuestForm", new RegisterGuestForm(Guid.Parse(this.Request.QueryString["partyGuid"])));
        }

        //// commented out until js client side validation wired-up
        //public JsonResult ValidateIsEmailAvailable(string emailAddress)
        //{it's
        //    return Json(this.Members.GetByUsername(emailAddress) == null, JsonRequestBehavior.AllowGet);
        //}

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

                        this.ModelState.AddModelError("EmailValidation", "Email already registered");

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

            // send cookie
            FormsAuthentication.SetAuthCookie(partyGuest.Username, true);

            return this.NavigateToRegisterGuestUrl(registerGuestForm.PartyGuid);
            //return this.RedirectToCurrentUmbracoPage();
        }

        [MemberAuthorize(AllowType=PartyGuest.Alias)]
        [ChildActionOnly]
        public ActionResult RenderRegisterGuestBillingForm()
        {
            return this.PartialView("RegisterGuestForm", new RegisterGuestBillingForm());
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

            // we have enough details to initiate the sage pay process
            
            // 1) post to sagepay, get response, and then redirect user

            // See: DonateSurfaceController HandleDonateForm()

            return this.CurrentUmbracoPage();
        }
    }
}

