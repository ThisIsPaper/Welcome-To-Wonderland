
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Media;

    public class PartyGuest : Partier, IPartier
    {
        // Member Type
        public const string Alias = "PartyGuest";

        // Properties
        public const string BillingAddressAlias = "billingAddress";
        public const string ForgottenPasswordGuidAlias = "forgottenPasswordGuid";
        public const string DotMailerIdAlias = "dotMailerId";
        public const string DotMailerRegistrationCompleteAlias = "dotMailerRegistrationComplete";
        public const string FacebookRegistrationAlias = "facebookRegistration";

        public PartyGuest(IPublishedContent content)
            : base(content)
        {
        }

        public Address BillingAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<string>(PartyGuest.BillingAddressAlias));
            }
            set
            {
                this.SetPropertyValue(PartyGuest.BillingAddressAlias, value.ToString());
            }
        }

        public Guid ForgottenPasswordGuid
        {
            get
            {
                return this.GetPropertyValue<Guid>(PartyGuest.ForgottenPasswordGuidAlias);
            }
            set
            {
                this.SetPropertyValue(PartyGuest.ForgottenPasswordGuidAlias, value.ToString("D").ToLower());
            }
        }

        public int DotMailerId
        {
            get
            {
                return this.GetPropertyValue<int>(PartyGuest.DotMailerIdAlias);
            }
            set
            {
                this.SetPropertyValue(PartyGuest.DotMailerIdAlias, value);
            }
        }

        public bool DotMailerRegistrationComplete
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyGuest.DotMailerRegistrationCompleteAlias);
            }
            set
            {
                this.SetPropertyValue(PartyGuest.DotMailerRegistrationCompleteAlias, value);
            }
        }

        public bool FacebookRegistration
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.FacebookRegistrationAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.FacebookRegistrationAlias, value);
            }
        }


        public string PartyUrl
        {
            get
            {
                return UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Party.Alias).Url + this.Members.GetPartyHost(this.PartyGuid).PartyUrlIdentifier;
            }
        }

        public static explicit operator PartyGuest(MembershipUser membershipUser)
        {
            return new PartyGuest(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
