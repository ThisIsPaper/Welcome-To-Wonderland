
namespace Wonderland.Logic.Models.Members
{
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Entities;

    public class PartyHost : BaseMemberType
    {
        // Member Type
        public const string Alias = "PartyHost";

        // Properties
        public const string MarketingSourceAlias = "marketingSource";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string PartyKitAddressAlias = "partyKitAddress";
        public const string BillingAddressAlias = "billingAddress";
        public const string HasRequestedPartyKitAlias = "hasRequestedPartyKit";
        public const string PartyUrlIdentifierAlias = "partyUrlIdentifier";

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

        //public bool IsPartyHost(Guid partyGuid)
        //{
        //    return false;
        //}

        //public bool IsPartyGuest(Guid partyGuid)
        //{
        //    return false;
        //}


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
