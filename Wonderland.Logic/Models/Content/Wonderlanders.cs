
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Members;

    public class Wonderlanders : SitePage
    {
        // Document Type
        public new const string Alias = "Wonderlanders";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string TopFundraisersHeadingAlias = "topFundraisersHeading";
        public const string TopFundraisersCountAlias = "topFundraisersCount";
        public const string MostGuestsHeadingAlias = "mostGuestsHeading";
        public const string MostGuestsCountAlias = "mostGuestsCount";
        public const string BadgesTodayHeadingAlias = "badgesTodayHeading";
        public const string RecentHostsHeadingAlias = "recentHostsHeading";
        public const string RecentHostsCountAlias = "recentHostsCount";

        public Wonderlanders(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Wonderlanders.PageHeadingAlias);
            }
        }

        public string TopFundraisersHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Wonderlanders.TopFundraisersHeadingAlias);
            }
        }

        public int TopFundraisersCount
        {
            get
            {
                return this.GetPropertyValue<int>(Wonderlanders.TopFundraisersCountAlias);
            }
        }

        public string MostGuestsHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Wonderlanders.MostGuestsHeadingAlias);
            }
        }

        public int MostGuestsCount
        {
            get
            {
                return this.GetPropertyValue<int>(Wonderlanders.MostGuestsCountAlias);
            }
        }

        public string BadgesTodayHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Wonderlanders.BadgesTodayHeadingAlias);
            }
        }

        public string RecentHostsHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Wonderlanders.RecentHostsHeadingAlias);
            }
        }

        public int RecentHostsCount
        {
            get
            {
                return this.GetPropertyValue<int>(Wonderlanders.RecentHostsCountAlias);
            }
        }

        public Leaderboard GetLeaderboard()
        {
            return this.Children<Leaderboard>().Single();
        }

        public IEnumerable<PartyHost> TopFundraisers
        {
            get
            {
                return this.Members.GetTopPartyHosts(LeaderboardType.TopFundraisers, this.TopFundraisersCount);
            }
        }

        public IEnumerable<PartyHost> MostGuests
        {
            get
            {
                return this.Members.GetTopPartyHosts(LeaderboardType.MostGuests, this.MostGuestsCount);
            }
        }

        //public IEnumerable<PartyHost> BadgesToday
        //{  
        //    get
        //    {
        //        return Enumerable.Empty<PartyHost>();
        //    }
        //}

        //public IEnumerable<PartyHost> RecentHosts
        //{
        //    get
        //    {
        //        return Enumerable.Empty<PartyHost>();
        //    }
        //}
    }
}
