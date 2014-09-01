
namespace Wonderland.Logic.Models.Members
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Umbraco.Web.Security;

    public class Partier : PublishedContentModel
    {
        public const string Alias = "Partier";
        public const string HostRoleAlias = "Party Host";
        public const string GuestRoleAlias = "Party Guest";

        public Partier(IPublishedContent content)
            : base(content)
        {
        }

        public static explicit operator Partier(Member member)
        {
            return new Partier(new MembershipHelper(UmbracoContext.Current).GetById(member.Id));
        }
    }
}
