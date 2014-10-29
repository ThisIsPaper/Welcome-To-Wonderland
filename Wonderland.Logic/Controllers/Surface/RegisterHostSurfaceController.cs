
namespace Wonderland.Logic.Controllers.Surface
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.Mvc;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;

    public class RegisterHostSurfaceController : SurfaceController
    {        
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

            return this.PartialView("RegisterHost/Forms/RegisterHostForm", new RegisterHostForm());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HandleRegisterHostForm(RegisterHostForm registerHostForm)
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

                        this.ModelState.AddModelError("RegisterHostValidation", "Email already registered");

                        break;
                }

                return this.CurrentUmbracoPage();
            }            

            // cast from MembershipUser rather than use this.Members.GetCurrentMember() helper (which needs a round trip for the login)
            PartyHost partyHost = (PartyHost)membershipUser;

            partyHost.FacebookRegistration = false;

            partyHost.MarketingSource = registerHostForm.MarketingSource;

            Guid partyGuid = Guid.NewGuid();

            this.DatabaseContext.Database.Insert(new PartyRow(partyGuid));

            // update database with member and party guid (duplicated data, but never changes)
            this.DatabaseContext.Database.Insert(new MemberPartyRow(partyHost.Id, partyGuid));

            // (duplicate data) store party guid in cms cache
            partyHost.PartyGuid = partyGuid;

            // set the default custom url to be the party guid
            partyHost.PartyUrlIdentifier = partyGuid.ToString();

            // set default party date
            partyHost.PartyDateTime = PartyHost.DefaultPartyDate;

            // add member to DotMailer
            DotMailerService.HostRegistrationStarted((Contact)partyHost);

            // send cookie
            FormsAuthentication.SetAuthCookie(partyHost.Username, true);

            // cause redirect, so that the login takes effect
            //return this.RedirectToCurrentUmbracoPage();
            return this.Redirect(this.CurrentPage.Url + "party-kit/");
        }

        [ChildActionOnly]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult RenderRegisterHostPartyKitForm()
        {   
            // get the renderModel for the current page
            RegisterHost model = (RegisterHost)this.CurrentPage;

            this.ViewBag.TShirtSizes = model.TShirtSizes;

            // set first / last name, as this will be available if user connected via facebook

            RegisterHostPartyKitForm registerHostPartyKitForm = new RegisterHostPartyKitForm();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            registerHostPartyKitForm.FirstName = partyHost.FirstName;
            registerHostPartyKitForm.LastName = partyHost.LastName;

            return this.PartialView("RegisterHost/Forms/RegisterHostPartyKitForm", registerHostPartyKitForm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [MemberAuthorize(AllowType = PartyHost.Alias)]
        public ActionResult HandleRegisterHostPartyKitForm(RegisterHostPartyKitForm registerHostPartyKitForm)
        {
            if (!ModelState.IsValid)
            {
                return this.CurrentUmbracoPage();
            }

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            if (partyHost.FirstName != registerHostPartyKitForm.FirstName)
            {
                partyHost.FirstName = registerHostPartyKitForm.FirstName;
            }

            if (partyHost.LastName != registerHostPartyKitForm.LastName)
            {
                partyHost.LastName = registerHostPartyKitForm.LastName;
            }
            
            Address address = new Address(
                                        registerHostPartyKitForm.Address1,
                                        registerHostPartyKitForm.Address2,
                                        registerHostPartyKitForm.TownCity,
                                        registerHostPartyKitForm.PostCode);

            partyHost.PartyKitAddress = address;
            partyHost.PartyAddress = address;
            partyHost.BillingAddress = address;

            partyHost.TShirtSize = registerHostPartyKitForm.TShirtSize;

            partyHost.HasRequestedPartyKit = true;            

            // update contact in DotMailer
            DotMailerService.HostRegistrationCompleted((Contact)partyHost);

            // mark as completed
            partyHost.DotMailerRegistrationComplete = true;

            //return this.CurrentUmbracoPage();
            return this.RedirectToCurrentUmbracoPage();
        }
    }
}

