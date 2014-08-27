
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

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
