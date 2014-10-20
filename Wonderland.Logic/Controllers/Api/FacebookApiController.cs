
namespace Wonderland.Logic.Controllers.Api
{
    using Facebook;
    using System;
    using System.Web.Http;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.Attributes;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Messages;

    public class FacebookApiController : UmbracoApiController
    {
        [HttpPost]
        [ValidateAntiForgeryToken]
        public FormResponse RegisterHost([FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();

            // using the supplied credentials query facebook to get email and names
            FacebookDetails facebookDetails = this.GetFacebookDetails(facebookCredentials);

            // no helper method on this.Members to register a user with a given memberType, so calling provider directly
            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = (UmbracoMembershipProviderBase)Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName];

            MembershipCreateStatus membershipCreateStatus;

            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                               PartyHost.Alias,                                // member type alias
                                               facebookDetails.EmailAddress,                   // username
                                               this.GetPassword(facebookDetails),              // password
                                               facebookDetails.EmailAddress,                   // email
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

                        formResponse.Errors = this.ModelState.GetErrors();
                        break;
                }

                return formResponse;
            }

            // cast from MembershipUser rather than use this.Members.GetCurrentMember() helper (which needs a round trip for the login)
            PartyHost partyHost = (PartyHost)membershipUser;

            partyHost.FacebookRegistration = true;

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

            formResponse.Success = true;

            formResponse.Message = this.Umbraco.TypedContentSingleAtXPath("//" + Wonderland.Logic.Models.Content.RegisterHost.Alias).Url;

            return formResponse;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FormResponse RegisterGuest([FromUri] Guid partyGuid, [FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();

            FacebookDetails facebookDetails = this.GetFacebookDetails(facebookCredentials);

            // no helper method on this.Members to register a user with a given memberType, so calling provider directly
            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = (UmbracoMembershipProviderBase)Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName];

            MembershipCreateStatus membershipCreateStatus;

            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                               PartyGuest.Alias,                               // member type alias
                                               facebookDetails.EmailAddress,                   // username
                                               this.GetPassword(facebookDetails),              // password
                                               facebookDetails.EmailAddress,                   // email
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

                        formResponse.Errors = this.ModelState.GetErrors();

                        break;
                }

                return formResponse;
            }

            // cast from MembershipUser rather than use this.Members.GetCurrentMember() helper (which needs a round trip for the login)
            PartyGuest partyGuest = (PartyGuest)membershipUser;

            partyGuest.FacebookRegistration = true;

            // update database with member and party guid (duplicated data, but never changes)
            this.DatabaseContext.Database.Insert(new MemberPartyRow(partyGuest.Id, partyGuid));

            // (duplicate data) store party guid in cms cache
            partyGuest.PartyGuid = partyGuid;

            // add member to DotMailer
            DotMailerService.GuestRegistrationStarted((Contact)partyGuest);

            // send cookie
            FormsAuthentication.SetAuthCookie(partyGuest.Username, true);

            formResponse.Success = true;

            formResponse.Message = this.Umbraco.TypedContentSingleAtXPath("//" + Wonderland.Logic.Models.Content.RegisterGuest.Alias).Url;

            return formResponse;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public FormResponse Login([FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();

            FacebookDetails facebookDetails = this.GetFacebookDetails(facebookCredentials);

            if (this.Members.Login(facebookDetails.EmailAddress, this.GetPassword(facebookDetails)))
            {
                formResponse.Success = true;

                formResponse.Message = ((IPartier)this.Members.GetByUsername(facebookDetails.EmailAddress)).PartyUrl;
            }
            else
            {
                this.ModelState.AddModelError("LoginValidation", "Unknown user");

                formResponse.Errors = this.ModelState.GetErrors();
            }

            return formResponse;
        }
       
        private FacebookDetails GetFacebookDetails(FacebookCredentials facebookCredentials)
        {
            FacebookClient facebookClient = new FacebookClient(facebookCredentials.AccessToken);

            dynamic me = facebookClient.Get("me");

            return new FacebookDetails()
                {
                    UserId = me.id,
                    EmailAddress = me.email,
                    FirstName = me.first_name,
                    LastName = me.last_name
                };
        }

        /// <summary>
        /// using the details requested from facebook, generate a password for this user
        /// </summary>
        /// <param name="facebookDetails"></param>
        /// <returns></returns>
        private string GetPassword(FacebookDetails facebookDetails)
        {
            // HACK: 
            string passwordPreHash = facebookDetails.UserId + "70E5B25D-8A18-4302-AE04-79CBD3FC1EE0";

            return FormsAuthentication.HashPasswordForStoringInConfigFile(passwordPreHash, "sha1");
        }

        private struct FacebookDetails
        {
            internal string UserId { get; set; }

            internal string EmailAddress { get; set; }

            internal string FirstName { get; set; }

            internal string LastName { get; set; }
        }
    }
}
