
namespace Wonderland.Logic.Models.Members
{
    using System;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Core.Services;
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

        public static Partier RegisterHost(string emailAddress, string password, string marketingSource)
        {
            IMemberService memberService = ApplicationContext.Current.Services.MemberService;

            if (memberService.GetByUsername(emailAddress) != null)
            {
                throw new Exception("Email Already Exists");
            }

            IMember member = memberService.CreateMember(emailAddress, emailAddress, emailAddress, Partier.Alias);

            memberService.SavePassword(member, password);

            memberService.Save(member);

            memberService.AssignRole(member.Id, Partier.HostRoleAlias);

            return (Partier)(Member)member;
        }

        public static Partier RegisterGuest(string emailAddress, string password, Guid partyGuid)
        {
            throw new NotImplementedException();
        }

        public static explicit operator Partier(Member member)
        {
            return new Partier(new MembershipHelper(UmbracoContext.Current).GetById(member.Id));
        }
    }
}
