
namespace Wonderland.Logic.Models.Members
{
    using Archetype.Models;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Entities;

    public class Partier : BaseMemberType
    {
        // Member Type
        public const string Alias = "Partier";

        // Properties
        public const string MarketingSourceAlias = "marketingSource";
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";
        public const string PartyKitAddressAlias = "partyKitAddress";
        public const string BillingAddressAlias = "billingAddress";
        public const string HasRequestedPartyKitAlias = "hasRequestedPartyKit";

        // Roles
        public const string HostRoleAlias = "Party Host";
        public const string GuestRoleAlias = "Party Guest";

        public Partier(IPublishedContent content)
            : base(content)
        {
        }

        public string MarketingSource
        {
            get
            {
                return this.GetPropertyValue<string>(Partier.MarketingSourceAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.MarketingSourceAlias, value);
            }
        }

        public string FirstName
        {
            get
            {
                return this.GetPropertyValue<string>(Partier.FirstNameAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.FirstNameAlias, value);
            }
        }

        public string LastName
        {
            get
            {
                return this.GetPropertyValue<string>(Partier.LastNameAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.LastNameAlias, value);
            }
        }

        /// <summary>
        /// only a host may have this populated
        /// </summary>
        public Address PartyKitAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<ArchetypeModel>(Partier.PartyKitAddressAlias).Single());
            }
            set 
            {
                this.SetPropertyValue(Partier.PartyKitAddressAlias, value.ToArchetypeModel());
            }
        }

        public Address BillingAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<ArchetypeModel>(Partier.BillingAddressAlias).Single());
            }
            set 
            {
                this.SetPropertyValue(Partier.BillingAddressAlias, value.ToArchetypeModel());
            }
        }

        public bool HasRequestedPartyKit
        {
            get
            {
                return this.GetPropertyValue<bool>(Partier.HasRequestedPartyKitAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.HasRequestedPartyKitAlias, value);
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

        //public bool IsInRole(string role)
        //{
        //    return false;
        //}
        
        /// <summary>
        /// Gets the Partier for the currently logged on member
        /// </summary>
        /// <returns>a Partier (if logged in) or null</returns>
        public static Partier GetCurrentPartier()
        {
            MembershipHelper membershipHelper = new MembershipHelper(UmbracoContext.Current);
            
            if (membershipHelper.GetCurrentLoginStatus() != null)
            {
                return new Partier(membershipHelper.GetCurrentMember());
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="membershipUser"></param>
        /// <returns></returns>
        public static explicit operator Partier(MembershipUser membershipUser)
        {
            return new Partier(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
