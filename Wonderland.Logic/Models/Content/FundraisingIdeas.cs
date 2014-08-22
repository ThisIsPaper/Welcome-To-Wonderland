
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class FundraisingIdeas : SitePage
    {
        public const string Alias = "FundraisingIdeas";

        public FundraisingIdeas(IPublishedContent content)
            : base(content)
        {
        }
    }
}
