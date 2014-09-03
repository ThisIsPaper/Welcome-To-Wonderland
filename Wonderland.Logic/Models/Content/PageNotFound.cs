
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class PageNotFound : SitePage
    {
        // DocumentType
        public new const string Alias = "PageNotFound";

        public PageNotFound(IPublishedContent content)
            : base(content)
        {
        }
    }
}
