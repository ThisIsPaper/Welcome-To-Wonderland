
namespace Wonderland.Logic.Models.Members
{
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Models.Entities;

    public class PartyGuest : BaseMemberType
    {
        // Member Type
        public const string Alias = "PartyGuest";

        // Properties

        public PartyGuest(IPublishedContent content)
            : base(content)
        {
        }

        public static explicit operator PartyGuest(MembershipUser membershipUser)
        {
            return new PartyGuest(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
