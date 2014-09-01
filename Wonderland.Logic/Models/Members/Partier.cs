
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
        }

        public string LastName
        {
            get
            {
                return this.GetPropertyValue<string>("lastName");
            }
        }

        /// <summary>
        /// only a host may have this populated
        /// </summary>
        public Address PartyKitAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<ArchetypeModel>("address").Single());
            }

            //http://our.umbraco.org/projects/backoffice-extensions/archetype/sound-off!/56042-How-to-add-archetype-values-programatically
            //set { }
        }

        public Address BillingAddress
        {
            get
            {
                return new Address(this.GetPropertyValue<ArchetypeModel>("address").Single());
            }
            //set { }
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
        public static Partier GetPartier()
        {
            MembershipHelper membershipHelper = new MembershipHelper(UmbracoContext.Current);

            if (membershipHelper.GetCurrentLoginStatus() != null)
            {
                return new Partier(membershipHelper.GetCurrentMember());
            }

            return null;
        }

        public static explicit operator Partier(MembershipUser membershipUser)
        {
            return Partier.GetPartier(membershipUser.UserName); // (ProviderUserKey is null during registration)
        }

        //public static explicit operator Partier(Member member)
        //{
        //    return Partier.GetPartier(member.Username);
        //}

        private static Partier GetPartier(string username)
        {
            return new Partier(new MembershipHelper(UmbracoContext.Current).GetByUsername(username));
        }
    }
}
