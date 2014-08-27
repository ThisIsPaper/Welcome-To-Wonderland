
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Legal : SitePage
    {
        public new const string Alias = "Legal";

        public Legal(IPublishedContent content)
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

        public string Copy
        {
            get
            {
                return this.GetPropertyValue<string>("copy");
            }
        }
    }
}
