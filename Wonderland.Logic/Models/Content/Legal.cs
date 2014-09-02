
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Legal : SitePage
    {
        // Document Type
        public new const string Alias = "Legal";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string CopyAlias = "copy";

        public Legal(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Legal.PageHeadingAlias);
            }
        }

        public string Copy
        {
            get
            {
                return this.GetPropertyValue<string>(Legal.CopyAlias);
            }
        }
    }
}
