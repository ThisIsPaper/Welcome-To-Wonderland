
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
    using Umbraco.Web.Security;
    using UmbracoExamine;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Members;
    
    public static class MembershipHelperExtensions
    {
        public static bool IsLoggedInPartier(this MembershipHelper members)
        {
            return members.IsLoggedIn() && members.GetCurrentMember() is IPartier;
        }

        public static IPartier GetCurrentPartier(this MembershipHelper members)
        {
            return members.GetCurrentMember() as IPartier;
        }

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

        public static IPartier GetPartier(this MembershipHelper members, Guid forgottenPasswordGuid)
        {
            //BaseSearchProvider searchProvider = ExamineManager.Instance.SearchProviderCollection["InternalMemberSearcher"];
            //ISearchCriteria searchCriteria = searchProvider.CreateSearchCriteria(IndexTypes.Member).Field("forgottenPasswordGuid", forgottenPasswordGuid.ToString()).Compile();
            //ISearchResults searchResults = searchProvider.Search(searchCriteria);

            //return searchResults.Select(x => (IPartier)members.GetById(x.Id)).SingleOrDefault();

            //// WARNING: hits db
            IMember member = ApplicationContext
                                .Current
                                .Services
                                .MemberService
                                .GetMembersByPropertyValue("forgottenPasswordGuid", forgottenPasswordGuid.ToString())
                                .SingleOrDefault();

            if (member != null)
            {
                return members.GetById(member.Id) as IPartier;
            }

            return null;
        }

        public static PartyHost GetPartyHost(this MembershipHelper members, Guid partyGuid)
        {
            BaseSearchProvider searchProvider = ExamineManager.Instance.SearchProviderCollection["InternalMemberSearcher"];
            ISearchCriteria searchCriteria = searchProvider.CreateSearchCriteria(IndexTypes.Member).Field("partyGuid", partyGuid.ToString()).And().NodeTypeAlias(PartyHost.Alias.ToLower()).Compile();
            ISearchResults searchResults = searchProvider.Search(searchCriteria);

            return searchResults.Select(x => (PartyHost)members.GetById(x.Id)).SingleOrDefault();

            //// WARNING: hits db            
            //IMember partyHost = ApplicationContext
            //                    .Current
            //                    .Services
            //                    .MemberService
            //                    .GetMembersByMemberType(PartyHost.Alias)
            //                    .SingleOrDefault(x => x.GetValue<Guid>(PartyHost.PartyGuidAlias) == partyGuid);

            //if (partyHost != null)
            //{
            //    return new PartyHost(members.GetByUsername(partyHost.Username));
            //}

            //return null;
        }

        public static PartyHost GetPartyHost(this MembershipHelper members, string partyUrlIdentifier)
        {
            BaseSearchProvider searchProvider = ExamineManager.Instance.SearchProviderCollection["InternalMemberSearcher"];
            ISearchCriteria searchCriteria = searchProvider.CreateSearchCriteria(IndexTypes.Member).Field("partyUrlIdentifier", partyUrlIdentifier).Compile();
            ISearchResults searchResults = searchProvider.Search(searchCriteria);

            return searchResults.Select(x => (PartyHost)members.GetById(x.Id)).SingleOrDefault();

            //// WARNING: hits db
            //IMember partyHost = ApplicationContext
            //                        .Current
            //                        .Services
            //                        .MemberService
            //                        .GetMembersByMemberType(PartyHost.Alias)
            //                        .SingleOrDefault(x => x.GetValue<string>(PartyHost.PartyUrlIdentifierAlias).ToLower() == partyUrlIdentifier.ToLower());

            //if (partyHost != null)
            //{
            //    return new PartyHost(members.GetByUsername(partyHost.Username));
            //}

            //return null;
        }

        public static IEnumerable<IPartier> GetAllPartiers(this MembershipHelper members)
        {
            List<IPartier> partiers = new List<IPartier>();

            partiers.AddRange(
                ApplicationContext
                    .Current
                    .Services
                    .MemberService
                    .GetMembersByMemberType(PartyHost.Alias)
                    .Select(x => (IPartier)members.GetById(x.Id))
                );

            partiers.AddRange(
                ApplicationContext
                    .Current
                    .Services
                    .MemberService
                    .GetMembersByMemberType(PartyGuest.Alias)
                    .Select(x => (IPartier)members.GetById(x.Id))
                );
            
            //DatabaseContext databaseContext = ApplicationContext.Current.DatabaseContext;

            //foreach(MemberPartyRow memberPartyRow in databaseContext.Database.Fetch<MemberPartyRow>(@"SELECT MemberId, PartyGuid, Timestamp FROM wonderlandMemberParty"))
            //{
            //    partiers.Add((IPartier)members.GetById(memberPartyRow.MemberId));
            //}

            return partiers;
        }


        /// <summary>
        /// TODO: consider refactoring this, so that the additional data of 'partier count' and 'amount raised' is returned and used
        /// </summary>
        /// <param name="members"></param>
        /// <param name="leaderboardType"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        public static IEnumerable<PartyHost> GetTopPartyHosts(this MembershipHelper members, LeaderboardType leaderboardType, int take)
        {
            List<PartyHost> partyHosts = new List<PartyHost>();

            PartyHost partyHost = null;

            DatabaseContext databaseContext = ApplicationContext.Current.DatabaseContext;

            switch (leaderboardType)
            {
                case LeaderboardType.MostGuests:

                    foreach(dynamic mostGuest in databaseContext.Database.Fetch<dynamic>(@"
                                                                                            SELECT      TOP " + take + @"  
                                                                                                        PartyGuid,
                                                                                                        COUNT(MemberId) AS Partiers
                                                                                            FROM        wonderlandMemberParty
                                                                                            GROUP BY    PartyGuid
                                                                                            ORDER BY    Partiers DESC
                                                                                        "))
                    {
                        partyHost = members.GetPartyHost((Guid)mostGuest.PartyGuid);
                        partyHost.TotalGuests = mostGuest.Partiers;
                        partyHosts.Add(partyHost);
                    }

                    break;

                case LeaderboardType.TopFundraisers:

                    foreach(dynamic topFundraiser in databaseContext.Database.Fetch<dynamic>(@"
                                                                                                SELECT      TOP " + take + @"
                                                                                                            PartyGuid,
                                                                                                            SUM(Amount) AS Amount
                                                                                                FROM        wonderlandDonation
                                                                                                WHERE       Success = 1
                                                                                                GROUP BY    PartyGuid
                                                                                                ORDER BY    Amount DESC
                                                                                            "))
                    {
                        partyHost = members.GetPartyHost((Guid)topFundraiser.PartyGuid);
                        partyHost.AmountRaised = topFundraiser.Amount;
                        partyHosts.Add(partyHost);
                    }                    

                    break;
            }

            return partyHosts;
        }        
    }
}
