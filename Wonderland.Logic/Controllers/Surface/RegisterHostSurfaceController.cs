﻿
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

    public class RegisterHostSurfaceController : SurfaceController
    {
        /// <summary>
        /// returns the partial for the host registration form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly] // (Cannot redirect from a Child Action - see Controllers\Render\RegisterHostController)
        public ActionResult Index()
        {
            // get the party for the current page
            RegisterHost registerHost = (RegisterHost)this.CurrentPage;

            // get marketing sources for a dropdown 
            List<string> marketingSources = new List<string>(registerHost.MarketingSources);

            // add an empty option the the beginning
            marketingSources.Insert(0, string.Empty);

            // add a dropdown to the viewbag
            this.ViewBag.MarketingSources = new SelectList(marketingSources);

            return this.PartialView("RegisterHostFormPartial", new RegisterHostForm());
        }

        //// commented out until js client side validation wired-up
        //public JsonResult ValidateIsEmailAvailable(string emailAddress)
        //{
        //    return Json(this.Members.GetByUsername(emailAddress) == null, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterHostForm(RegisterHostForm registerHostForm)
        {
            if (!this.ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            // no helper method on this.Members to register a user with a given memberType, so calling provider directly
            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = (UmbracoMembershipProviderBase)Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName];

            MembershipCreateStatus membershipCreateStatus;
            
            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                                PartyHost.Alias,                                // member type alias
                                                registerHostForm.EmailAddress,                  // username
                                                registerHostForm.Password,                      // password
                                                registerHostForm.EmailAddress,                  // email
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
            PartyHost partyHost = (PartyHost)membershipUser;

            partyHost.MarketingSource = registerHostForm.MarketingSource;

            partyHost.PartyUrlIdentifier = Guid.NewGuid().ToString();

            // send cookie
            FormsAuthentication.SetAuthCookie(partyHost.Username, true);

            return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias));
        }
    }
}

