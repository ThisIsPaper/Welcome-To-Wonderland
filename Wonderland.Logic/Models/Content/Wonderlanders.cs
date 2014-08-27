
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Members;

    public class Wonderlanders : SitePage
    {
        public new const string Alias = "Wonderlanders";

        public Wonderlanders(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>("pageHeading");
            }
        }

        public string TopFundraisersHeading
        {
            get
            {
                return this.GetPropertyValue<string>("topFundraisersHeading");
            }
        }

        public int TopFundraisersCount
        {
            get
            {
                return this.GetPropertyValue<int>("topFundraisersCount");
            }
        }

        public string MostGuestsHeading
        {
            get
            {
                return this.GetPropertyValue<string>("mostGuestsHeading");
            }
        }

        public int MostGuestsCount
        {
            get
            {
                return this.GetPropertyValue<int>("mostGuestsCount");
            }
        }

        public string BadgesTodayHeading
        {
            get
            {
                return this.GetPropertyValue<string>("badgesTodayHeading");
            }
        }

        public string RecentHostsHeading
        {
            get
            {
                return this.GetPropertyValue<string>("recentHostsHeading");
            }
        }

        public int RecentHostsCount
        {
            get
            {
                return this.GetPropertyValue<int>("recentHostsCount");
            }
        }

        public Leaderboard GetLeaderboard()
        {
            return this.Children<Leaderboard>().Single();
        }

        public IEnumerable<Partier> GetTopFundraiserPartiers()
        {
            return Enumerable.Empty<Partier>();
        }

        public IEnumerable<Partier> GetMostGuestPartiers()
        {
            return Enumerable.Empty<Partier>();
        }

        public IEnumerable<Partier> GetBadgesTodayPartiers()
        {
            return Enumerable.Empty<Partier>();
        }

        public IEnumerable<Partier> GetRecentHostPartiers()
        {
            return Enumerable.Empty<Partier>();
        }
    }
}
