
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.Models.Content;
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
            return this.PartialView("RegisterGuestFormPartial", new RegisterGuestForm(Guid.Parse(this.Request.QueryString["partyGuid"])));
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
            PartyGuest partyguest = (PartyGuest)membershipUser;

            partyguest.PartyGuid = registerGuestForm.PartyGuid;

            // send cookie
            FormsAuthentication.SetAuthCookie(partyguest.Username, true);

            return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterGuestConfirmation.Alias));
        }
    }
}

