
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

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
    }
}
