
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class PistePoints : SitePage
    {
        public const string Alias = "PistePoints";

        public PistePoints(IPublishedContent content)
            : base(content)
        {
        }
    }
}
