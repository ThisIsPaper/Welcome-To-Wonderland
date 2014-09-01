
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
        public const string Alias = "Partier";
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
                return this.GetPropertyValue<string>("marketingSource");
            }
            set
            {
                this.SetPropertyValue("marketingSource", value);
            }
        }

        public string FirstName
        {
            get
            {
                return this.GetPropertyValue<string>("firstName");
            }
            set
            {
                this.SetPropertyValue("firstName", value);
            }
        }

        public string LastName
        {
            get
            {
                return this.GetPropertyValue<string>("lastName");
            }
            set
            {
                this.SetPropertyValue("lastName", value);
            }
        }

        /// <summary>
        /// only a host may have this populated
        /// </summary>
        public Address PartyKitAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<ArchetypeModel>("partyKitAddress").Single());
            }
            set 
            {
                this.SetPropertyValue("partyKitAddress", value.ToArchetypeModel());
            }
        }

        public Address BillingAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<ArchetypeModel>("billingAddress").Single());
            }
            set 
            {
                this.SetPropertyValue("billingAddress", value.ToArchetypeModel());
            }
        }

        public bool HasRequestedPartyKit
        {
            get
            {
                return this.GetPropertyValue<bool>("hasRequestedPartyKit");
            }
            set
            {
                this.SetPropertyValue("hasRequestedPartyKit", value);
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
