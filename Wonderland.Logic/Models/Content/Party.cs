
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Party : SitePage
    {
        public const string Alias = "Party";

        public Party(IPublishedContent content)
            : base(content)
        {
        }
    }
}
