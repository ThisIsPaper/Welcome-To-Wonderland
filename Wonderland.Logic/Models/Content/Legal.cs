
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Legal : SitePage
    {
        public const string Alias = "Legal";

        public Legal(IPublishedContent content)
            : base(content)
        {
        }
    }
}
