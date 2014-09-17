
namespace Wonderland.Logic.Extensions
{
    using Examine;
    using Examine.Providers;
    using Examine.SearchCriteria;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web.Security;
    using UmbracoExamine;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Members;

    public static class MembershipHelperExtensions
    {
        /// <summary>
        /// queries the Lucene index to get all members with the supplied partyGuid
        /// </summary>
        /// <param name="membershipHelper">the Umbraco MembershipHelper</param>
        /// <param name="partyGuid">Guid identifying a specific party</param>
        /// <returns>a collection of PartyHost / PartyGuest associated with a specific party</returns>
        public static IEnumerable<IPartier> GetPartiers(this MembershipHelper membershipHelper, Guid partyGuid)
        {
            BaseSearchProvider searchProvider = ExamineManager.Instance.SearchProviderCollection["InternalMemberSearcher"];
            ISearchCriteria searchCriteria = searchProvider.CreateSearchCriteria(IndexTypes.Member).Field("partyGuid", partyGuid.ToString()).Compile();
            ISearchResults searchResults = searchProvider.Search(searchCriteria);

            return searchResults.Select(x => (IPartier)membershipHelper.GetById(x.Id));
        }

        public static IEnumerable<PartyHost> GetTopPartyHosts(this MembershipHelper membershipHelper, LeaderboardType leaderboardType, int take)
        {
            return Enumerable.Empty<PartyHost>();
        }

        

    }
}
