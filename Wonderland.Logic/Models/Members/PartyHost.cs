
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;

    public class PartyHost : BaseMemberType, IPartier
    {
        // Member Type
        public const string Alias = "PartyHost";

        // Properties
        public const string PartyGuidAlias = "partyGuid";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string BillingAddressAlias = "billingAddress";
        public const string ProfileImageAlias = "profileImage";
        public const string BlockedAlias = "blocked";
        public const string MarketingSourceAlias = "marketingSource";
        public const string PartyKitAddressAlias = "partyKitAddress";
        public const string PartyAddressAlias = "partyAddress";
        public const string PartyDateTimeAlias = "partyDateTime";
        public const string HasRequestedPartyKitAlias = "hasRequestedPartyKit";        
        public const string PartyUrlIdentifierAlias = "partyUrlIdentifier";
        public const string PartyImageAlias = "partyImage";
        public const string PartyHeadingAlias = "partyHeading";
        public const string PartyCopyAlias = "partyCopy";
        public const string SuggestedDonationAlias = "suggestedDonation";
        public const string FundraisingTargetAlias = "fundraisingTarget";

        public PartyHost(IPublishedContent content)
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
                return this.GetPropertyValue<Guid>(PartyHost.PartyGuidAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyGuidAlias, value.ToString());
            }
        }

        public string FirstName
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.FirstNameAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.FirstNameAlias, value);
            }
        }

        public string LastName
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.LastNameAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.LastNameAlias, value);
            }
        }

        public Address BillingAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<string>(PartyHost.BillingAddressAlias));
            }
            set
            {
                this.SetPropertyValue(PartyHost.BillingAddressAlias, value.ToString());
            }
        }

        public string ProfileImage
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.ProfileImageAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.ProfileImageAlias, value);
            }
        }

        public bool Blocked
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.BlockedAlias);
            }
        }

        public string MarketingSource
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.MarketingSourceAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.MarketingSourceAlias, value);
            }
        }

        public Address PartyKitAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<string>(PartyHost.PartyKitAddressAlias));
            }
            set 
            {
                this.SetPropertyValue(PartyHost.PartyKitAddressAlias, value.ToString());
            }
        }

        public Address PartyAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<string>(PartyHost.PartyAddressAlias));
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyAddressAlias, value.ToString());
            }
        }

        public DateTime PartyDateTime
        {
            get
            {
                return this.GetPropertyValue<DateTime>(PartyHost.PartyDateTimeAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyDateTimeAlias, value.ToString());
            }
        }

        public bool HasRequestedPartyKit
        {
            get
            {
                return this.GetPropertyValue<bool>(PartyHost.HasRequestedPartyKitAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.HasRequestedPartyKitAlias, value);
            }
        }

        /// <summary>
        /// defaults to the party guid, but TODO: feature to allow for a custom url for a party
        /// </summary>
        public string PartyUrlIdentifier
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyUrlIdentifierAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyUrlIdentifierAlias, value);
            }
        }

        public string PartyImage
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyImageAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyImageAlias, value);
            }
        }

        public string PartyImageUrl
        {
            get
            {
                return "/Uploads/PartyImage/" + this.PartyImage;
            }
        }

        public string PartyHeading
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyHeadingAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyHeadingAlias, value);
            }
        }

        public string PartyCopy
        {
            get
            {
                return this.GetPropertyValue<string>(PartyHost.PartyCopyAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.PartyCopyAlias, value);
            }
        }

        public decimal SuggestedDonation
        {
            get
            {
                return this.GetPropertyValue<decimal>(PartyHost.SuggestedDonationAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.SuggestedDonationAlias, value.ToString());
            }
        }

        public decimal FundraisingTarget
        {
            get
            {
                return this.GetPropertyValue<decimal>(PartyHost.FundraisingTargetAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.FundraisingTargetAlias, value.ToString());
            }
        }

        public string ProfileImageUrl
        {
            get
            {
                return "/Uploads/Profile/" + this.ProfileImage;
            }          
        }

        public string PartyUrl
        {
            get
            {
                return UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Party.Alias).Url + this.PartyUrlIdentifier;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="membershipUser"></param>
        /// <returns></returns>
        public static explicit operator PartyHost(MembershipUser membershipUser)
        {
            return new PartyHost(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
