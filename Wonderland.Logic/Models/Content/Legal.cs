
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class Legal : SitePage
    {
        public new const string Alias = "Legal";

        public Legal(IPublishedContent content)
            : base(content)
        {
        }
    }
}
