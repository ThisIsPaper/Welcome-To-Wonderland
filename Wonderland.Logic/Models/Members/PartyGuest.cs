
namespace Wonderland.Logic.Models.Members
{
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;

    public class PartyGuest : Partier, IPartier
    {
        // Member Type
        public const string Alias = "PartyGuest";

        public PartyGuest(IPublishedContent content)
            : base(content)
        {
        }

        public string PartyUrl
        {
            get
            {
                return UmbracoContext.Current.ContentCache.GetSingleByXPath("//" + Party.Alias).Url + this.Members.GetPartyHost(this.PartyGuid).PartyUrlIdentifier;
            }
        }

        public static explicit operator PartyGuest(MembershipUser membershipUser)
        {
            return new PartyGuest(new MembershipHelper(UmbracoContext.Current).GetByUsername(membershipUser.UserName));
        }
    }
}
