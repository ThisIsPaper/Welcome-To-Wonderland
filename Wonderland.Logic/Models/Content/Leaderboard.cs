
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Leaderboard : SitePage
    {
        public const string Alias = "Leaderboard";

        public Leaderboard(IPublishedContent content)
            : base(content)
        {
        }
    }
}
