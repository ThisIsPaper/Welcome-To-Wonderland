
namespace Wonderland.Logic.Controllers.Api
{
    using System;
    using System.Web.Http;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Security;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Messages;

    public class FacebookApiController : UmbracoApiController
    {
        /// <summary>
        /// salt used to generate a password from the supplied facebook user id
        /// </summary>
        private const string Salt = "70E5B25D-8A18-4302-AE04-79CBD3FC1EE0";

        [HttpPost]
        public FormResponse RegisterHost([FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();
           
            // no helper method on this.Members to register a user with a given memberType, so calling provider directly
            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = (UmbracoMembershipProviderBase)Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName];

            MembershipCreateStatus membershipCreateStatus;

            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                               PartyHost.Alias,                                // member type alias
                                               facebookCredentials.EmailAddress,               // username
                                               this.GetPassword(facebookCredentials),          // password
                                               facebookCredentials.EmailAddress,               // email
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
            partyHost.PartyDateTime = new DateTime(2014, 12, 5, 20, 0, 0);

            // add member to DotMailer
            DotMailerService.HostRegistrationStarted((Contact)partyHost);

            // send cookie
            FormsAuthentication.SetAuthCookie(partyHost.Username, true);

            formResponse.Success = true;

            formResponse.Message = this.Umbraco.TypedContentSingleAtXPath("//" + Wonderland.Logic.Models.Content.RegisterHost.Alias).Url;

            return formResponse;
        }

        [HttpPost]
        public FormResponse RegisterGuest([FromUri] Guid partyGuid, [FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();

            // no helper method on this.Members to register a user with a given memberType, so calling provider directly
            UmbracoMembershipProviderBase membersUmbracoMembershipProvider = (UmbracoMembershipProviderBase)Membership.Providers[Constants.Conventions.Member.UmbracoMemberProviderName];

            MembershipCreateStatus membershipCreateStatus;

            MembershipUser membershipUser = membersUmbracoMembershipProvider.CreateUser(
                                               PartyGuest.Alias,                               // member type alias
                                               facebookCredentials.EmailAddress,               // username
                                               this.GetPassword(facebookCredentials),          // password
                                               facebookCredentials.EmailAddress,               // email
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
        public FormResponse Login([FromBody] FacebookCredentials facebookCredentials)
        {
            FormResponse formResponse = new FormResponse();

            if (this.Members.Login(facebookCredentials.EmailAddress, this.GetPassword(facebookCredentials)))
            {
                formResponse.Success = true;

                formResponse.Message = ((IPartier)this.Members.GetByUsername(facebookCredentials.EmailAddress)).PartyUrl;
            }

            return formResponse;
        }

        private string GetPassword(FacebookCredentials facebookCredentials)
        {
            return facebookCredentials.UserId + facebookCredentials.LastName;
        }
    }
}
