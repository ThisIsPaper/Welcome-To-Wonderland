
namespace Wonderland.Logic.Models.Members
{
    using Wonderland.Logic.Models.Entities;
    using Archetype.Models;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using System.Linq;
    using System.Web.Security;
    using System;

    public class Partier : PublishedContentModel
    {
        public const string Alias = "Partier";
        public const string HostRoleAlias = "Party Host";
        public const string GuestRoleAlias = "Party Guest";

        private IMember member = null;

        public Partier(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// helper to get at the MemberService
        /// </summary>
        internal IMemberService MemberService
        {
            get
            {
                return ApplicationContext.Current.Services.MemberService;
            }
        }

        /// <summary>
        /// lazy load the member obj used to set properties to the db
        /// </summary>
        internal IMember Member
        {
            get
            {
                if (this.member == null)
                {
                    this.member = this.MemberService.GetById(this.Id);
                }

                return this.member;
            }
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

        public bool IsPartyHost(Guid partyGuid)
        {
            return false;
        }

        public bool IsPartyGuest(Guid partyGuid)
        {
            return false;
        }

        public bool IsInRole(string role)
        {
            return false;
        }

        public void AssignRole(string role)
        {
            this.MemberService.AssignRole(this.Id, role);
        }
        
        //internal void Login()
        //{     
        //    // TODO:
        //    //// set memeber as online
        //    //membersUmbracoMembershipProvider.GetUser(registerHostForm.EmailAddress, true);

        //    FormsAuthentication.SetAuthCookie(this.Username, true);
        //}

        private void SetPropertyValue(string propertyAlias, object propertyValue)
        {
            this.Member.SetValue(propertyAlias, propertyValue);
            this.MemberService.Save(this.member, false); // false indicates that any Umbraco events watching member changes wont get fired
        }        

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

        private static Partier GetPartier(string username)
        {
            return new Partier(new MembershipHelper(UmbracoContext.Current).GetByUsername(username));
        }

        public static explicit operator Partier(MembershipUser membershipUser)
        {
            // using .UserName instead of .ProviderUserKey (as .ProviderUserKey is null during registration)            
            return Partier.GetPartier(membershipUser.UserName);
        }

        public static explicit operator Partier(Member member)
        {
            return Partier.GetPartier(member.Username);
        }
    }
}
