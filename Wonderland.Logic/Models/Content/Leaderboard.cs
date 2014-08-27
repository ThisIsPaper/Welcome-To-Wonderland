
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Members;
    using System.Web;
using System;

    public class Leaderboard : SitePage
    {
        public new const string Alias = "Leaderboard";

        public Leaderboard(IPublishedContent content)
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

        public int LeaderboardCount
        {
            get
            {
                return this.GetPropertyValue<int>("leaderboardCount");
            }
        }

        public LeaderboardType LeaderboardType
        {
            get
            {
                try
                {
                    return (LeaderboardType)Enum.Parse(typeof(LeaderboardType), HttpContext.Current.Request.QueryString.ToString(), true);
                }
                catch
                {
                    return LeaderboardType.Unknown;
                }                
            }
        }

        public string MostGuestsLink
        {
            get
            {
                return this.Url + "?" + LeaderboardType.MostGuests;
            }
        }

        public string TopFundraisersLink
        {
            get
            {
                return this.Url + "?" + LeaderboardType.TopFundraisers;
            }
        }

        public IEnumerable<Partier> GetPartiers()
        {
            // TODO: based on the current LeaderboardType
            return Enumerable.Empty<Partier>();
        }
    }
}
