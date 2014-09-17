
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
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostSurfaceController : SurfaceController
    {        
        /// <summary>
        /// Handles inbound links to the register host page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult NavigateToRegisterHostUrl()
        {
            return this.Redirect(this.Umbraco.TypedContentSingleAtXPath("//" + RegisterHost.Alias).Url);
        }

        /// <summary>
        /// returns the partial for the host registration form
        /// </summary>
        /// <returns></returns>
        [ChildActionOnly] // (Cannot redirect from a Child Action)
        public ActionResult RenderRegisterHostForm()
        {
            // get the renderModel for the current page
            RegisterHost model = (RegisterHost)this.CurrentPage;

            // get marketing sources for a dropdown 
            List<string> marketingSources = new List<string>(model.MarketingSources);

            // add an empty option the the beginning
            marketingSources.Insert(0, string.Empty);

            // add a dropdown to the viewbag
            this.ViewBag.MarketingSources = new SelectList(marketingSources);

            return this.PartialView("RegisterHostForm", new RegisterHostForm());
        }

        //// commented out until js client side validation wired-up
        //public JsonResult ValidateIsEmailAvailable(string emailAddress)
        //{
        //    return Json(this.Members.GetByUsername(emailAddress) == null, JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleRegisterHostForm(RegisterHostForm registerHostForm)
        {
            if (!this.ModelState.IsValid)
            {
                return View("RegisterHost/RegisterHost");
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

                return View("RegisterHost/RegisterHost");
            }            

            // cast from MembershipUser rather than use this.Members.GetCurrentMember() helper (which needs a round trip for the login)
            PartyHost partyHost = (PartyHost)membershipUser;

            partyHost.MarketingSource = registerHostForm.MarketingSource;

            Guid partyGuid = Guid.NewGuid();

            // update database with member and party guid (duplicated data, but never changes)
            this.ApplicationContext.DatabaseContext.Database.Insert(new MemberParty(partyHost.Id, partyGuid));

            // (duplicate data) store party guid in cms cache
            partyHost.PartyGuid = partyGuid;

            // set the default custom url to be the party guid
            partyHost.PartyUrlIdentifier = partyGuid.ToString();

            // send cookie
            FormsAuthentication.SetAuthCookie(partyHost.Username, true);

            return View("RegisterHost/RegisterHostPartyKit");
            //return this.CurrentUmbracoPage();
            //return this.RedirectToUmbracoPage(this.CurrentPage.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyKit.Alias));
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderRegisterHostPartyKitForm()
        {
            return this.PartialView("RegisterHostPartyKitForm", new RegisterHostPartyKitForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult HandleRegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterHost/RegisterHostPartyKit");
            }

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            partyHost.FirstName = registerHostPartyKitForm.FirstName;
            partyHost.LastName = registerHostPartyKitForm.LastName;

            Address address = new Address()
                                    {
                                        Address1 = registerHostPartyKitForm.Address1.Replace(Environment.NewLine, string.Empty),
                                        Address2 = registerHostPartyKitForm.Address2.Replace(Environment.NewLine, string.Empty),
                                        TownCity = registerHostPartyKitForm.TownCity.Replace(Environment.NewLine, string.Empty),
                                        Postcode = registerHostPartyKitForm.PostCode.Replace(Environment.NewLine, string.Empty)
                                    };

            partyHost.PartyKitAddress = address;
            partyHost.PartyAddress = address;

            partyHost.HasRequestedPartyKit = true;

            return View("RegisterHost/RegisterHostPartyUrl");
            //return this.CurrentUmbracoPage();
            //return this.RedirectToUmbracoPage(this.CurrentPage.Parent.Children.Single(x => x.DocumentTypeAlias == RegisterHostPartyUrl.Alias));
        }
    }
}

