﻿
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Content;

    public class PartyHost : BaseMemberType, IPartier
    {
        // Member Type
        public const string Alias = "PartyHost";

        // Properties
        public const string MarketingSourceAlias = "marketingSource";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string PartyKitAddressAlias = "partyKitAddress";
        public const string PartyAddressAlias = "partyAddress";
        public const string PartyDateTimeAlias = "partyDateTime";
        public const string HasRequestedPartyKitAlias = "hasRequestedPartyKit";
        public const string PartyUrlIdentifierAlias = "partyUrlIdentifier";
        public const string PartyHeadingAlias = "partyHeading";
        public const string PartyCopyAlias = "partyCopy";

        public PartyHost(IPublishedContent content)
            : base(content)
        {
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
                this.SetPropertyValue(PartyHost.PartyDateTimeAlias, value);
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

        //public bool IsPartyHost(Guid partyGuid)
        //{
        //    return false;
        //}

        //public bool IsPartyGuest(Guid partyGuid)
        //{
        //    return false;
        //}

        public string PartyUrl
        {
            get
            {
                return UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Party.Alias).Url + this.PartyUrlIdentifier;
            }
        }

        public string PersonName
        {
            get
            {
                return this.FirstName + " " + this.LastName;
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
