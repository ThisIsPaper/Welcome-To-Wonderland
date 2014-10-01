
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class WhoIsItFor : SitePage
    {
        // Document Type
        public new const string Alias = "WhoIsItFor";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string YouTubeCodeAlias = "youTubeCode";
        public const string CopyAlias = "copy";

        public WhoIsItFor(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(WhoIsItFor.PageHeadingAlias);
            }
        }

        public string YouTubeCode
        {
            get
            {
                return this.GetPropertyValue<string>(WhoIsItFor.YouTubeCodeAlias);
            }
        }

        public string Copy
        {
            get
            {
                return this.GetPropertyValue<string>(WhoIsItFor.CopyAlias);
            }
        }
    }
}
