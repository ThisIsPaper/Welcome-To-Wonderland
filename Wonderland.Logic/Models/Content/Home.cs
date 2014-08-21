
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Web;
    using Umbraco.Core.Models;

    public class Home : SitePage
    {
        public Home(IPublishedContent content) : base(content)
        {
        }

        public string BannerHeading
        {
            get
            {
                return this.GetPropertyValue<string>("bannerHeading");
            }
        }

        public string BannerCopy
        {
            get
            {
                return this.GetPropertyValue<string>("bannerCopy");
            }
        }
    }
}
