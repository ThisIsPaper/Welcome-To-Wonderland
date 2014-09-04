
namespace Wonderland.Logic.Models.Members
{
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Entities;

    public class PartyGuest : BaseMemberType, IPartier
    {
        // Member Type
        public const string Alias = "PartyGuest";

        // Properties
        public const string FirstNameAlias = "firstName";
        public const string LastNameAlias = "lastName";

        public PartyGuest(IPublishedContent content)
            : base(content)
        {
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

        public string PartyUrl
        {
            get
            {
                // TODO:
                return "/";
            }            
        }

        public string PersonName
        {
            get
            {
                // TODO:
                return "unknown guest";
            }
        }


        public static explicit operator PartyGuest(MembershipUser membershipUser)
        {
            return new PartyGuest(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }

    }
}
