
namespace Wonderland.Logic.Extensions
{
    using Examine;
    using Examine.Providers;
    using Examine.SearchCriteria;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Web;
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
        /// <param name="members">the Umbraco MembershipHelper</param>
        /// <param name="partyGuid">Guid identifying a specific party</param>
        /// <returns>a collection of PartyHost / PartyGuest associated with a specific party</returns>
        public static IEnumerable<IPartier> GetPartiers(this MembershipHelper members, Guid partyGuid)
        {
            BaseSearchProvider searchProvider = ExamineManager.Instance.SearchProviderCollection["InternalMemberSearcher"];
            ISearchCriteria searchCriteria = searchProvider.CreateSearchCriteria(IndexTypes.Member).Field("partyGuid", partyGuid.ToString()).Compile();
            ISearchResults searchResults = searchProvider.Search(searchCriteria);

            return searchResults.Select(x => (IPartier)members.GetById(x.Id));
        }

        public static PartyHost GetPartyHost(this MembershipHelper members, Guid partyGuid)
        {
            // WARNING: hits db            
            IMember partyHost = ApplicationContext
                                .Current
                                .Services
                                .MemberService
                                .GetMembersByMemberType(PartyHost.Alias)
                                .SingleOrDefault(x => x.GetValue<Guid>(PartyHost.PartyGuidAlias) == partyGuid);

            if (partyHost != null)
            {
                return new PartyHost(new MembershipHelper(UmbracoContext.Current).GetByUsername(partyHost.Username));
            }

            return null;
        }

        public static IEnumerable<PartyHost> GetTopPartyHosts(this MembershipHelper members, LeaderboardType leaderboardType, int take)
        {
            //UmbracoContext.Current.Application.DatabaseContext.Database

            switch (leaderboardType)
            {
                case LeaderboardType.MostGuests:

                    //SELECT    TOP 123
                    //          PartyGuid, 
                    //          Count(MemberID) AS 'Partiers'
                    //FROM		wonderlandMemberParty
                    //GROUP BY	PartyGuid
                    //ORDER BY	Partiers DESC

                    break;

                case LeaderboardType.TopFundraisers:
                    

                    

                    break;

            }


            return Enumerable.Empty<PartyHost>();
        }
        
    }
}
