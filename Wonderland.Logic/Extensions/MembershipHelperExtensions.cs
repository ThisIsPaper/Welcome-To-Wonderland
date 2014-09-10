
namespace Wonderland.Logic.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Members;

    public static class MembershipHelperExtensions
    {
        public static IEnumerable<IPartier> GetPartiers(this MembershipHelper membershipHelper, Guid partyGuid)
        {
            return Enumerable.Empty<IPartier>();
        }

        public static IEnumerable<PartyHost> GetTopPartyHosts(this MembershipHelper membershipHelper, LeaderboardType leaderboardType, int take)
        {
            return Enumerable.Empty<PartyHost>();
        }

    }
}
