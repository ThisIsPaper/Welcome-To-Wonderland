
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

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

        private int TopFundraisersCount
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

        public IEnumerable<Member> GetTopFundraisers()
        {
            return Enumerable.Empty<Member>();
        }

        public IEnumerable<Member> GetMostGuests()
        {
            return Enumerable.Empty<Member>();
        }

        public IEnumerable<Member> GetBadgesToday()
        {
            return Enumerable.Empty<Member>();
        }

        public IEnumerable<Member> GetRecentHosts()
        {
            return Enumerable.Empty<Member>();
        }
    }
}
