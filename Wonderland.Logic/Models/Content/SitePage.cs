
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Wonderland.Logic.Models.Entities;

    public class SitePage : PublishedContentModel
    {
        public SitePage(IPublishedContent content) : base(content)
        {
        }

        public string MetaDescription
        {
            get
            {
                return this.GetPropertyValue<string>("metaDescription");
            }
        }

        public SiteSettings SiteSettings
        {
            get
            {
                return new SiteSettings(new Home(this.AncestorOrSelf(Home.Alias)));
            }
        }
    }
}
