
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Members;

    public class Leaderboard : SitePage
    {
        // Document Type
        public new const string Alias = "Leaderboard";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string LeaderboardCountAlias = "leaderboardCount";

        public Leaderboard(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Leaderboard.PageHeadingAlias);
            }
        }

        public int LeaderboardCount
        {
            get
            {
                return this.GetPropertyValue<int>(Leaderboard.LeaderboardCountAlias);
            }
        }

        public LeaderboardType LeaderboardType
        {
            get;
            internal set;
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
