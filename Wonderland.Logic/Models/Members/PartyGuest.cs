
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

    public class PartyGuest : BaseMemberType, IPartier
    {
        // Member Type
        public const string Alias = "PartyGuest";

        // Properties
        public const string PartyGuidAlias = "partyGuid";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string BillingAddressAlias = "billingAddress";
        public const string ProfileImageAlias = "profileImage";
        public const string ForgottenPasswordGuidAlias = "forgottenPasswordGuid";
        public const string DotMailerIdAlias = "dotMailerId";
        public const string DotMailerRegistrationCompleteAlias = "dotMailerRegistrationComplete";


        private Guid? partyGuid = null; // used as a local cache, as the setter / getter normally requires a full round trip
        private string firstName = null;
        private string lastName = null;

        public PartyGuest(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// once created, never changes - this guid identifies a specific party
        /// </summary>
        public Guid PartyGuid
        {
            get
            {
                if (this.partyGuid.HasValue)
                {
                    return this.partyGuid.Value;
                }

                return this.GetPropertyValue<Guid>(PartyHost.PartyGuidAlias);
            }
            set
            {
                this.partyGuid = value;
                this.SetPropertyValue(PartyHost.PartyGuidAlias, value.ToString());
            }
        }

        public string FirstName
        {
            get
            {
                if (this.firstName != null)
                {
                    return this.firstName;
                }

                return this.GetPropertyValue<string>(PartyHost.FirstNameAlias);
            }
            set
            {
                this.firstName = value;
                this.SetPropertyValue(PartyHost.FirstNameAlias, value);
            }
        }

        public string LastName
        {
            get
            {
                if (this.lastName != null)
                {
                    return this.lastName;
                }

                return this.GetPropertyValue<string>(PartyHost.LastNameAlias);
            }
            set
            {
                this.lastName = value;
                this.SetPropertyValue(PartyHost.LastNameAlias, value);
            }
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

        public string ProfileImage
        {
            get
            {
                return this.GetPropertyValue<string>(PartyGuest.ProfileImageAlias);
            }
            set
            {
                this.SetPropertyValue(PartyGuest.ProfileImageAlias, value);
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

        public string ProfileImageUrl
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(this.ProfileImage))
                {
                    return "/Uploads/Profile/" + this.ProfileImage;
                }

                return null;                
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
