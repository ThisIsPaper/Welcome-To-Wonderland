
namespace Wonderland.Logic.Models.Content
{
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;

    public class SitePage : BaseContentType
    {
        // Document Type
        public const string Alias = "SitePage";

        // Properties
        public const string MetaDescriptionAlias = "metaDescription";

        private SiteSettings siteSettings = null;

        public SitePage(IPublishedContent content) : base(content)
        {
            this.ShowHeaderAndFooter = true; // not cms managed
        }

        public bool ShowHeaderAndFooter
        {
            get;
            internal set;
        }

        public string MetaDescription
        {
            get
            {
                return this.GetPropertyValue<string>(SitePage.MetaDescriptionAlias);
            }
        }

        public SiteSettings SiteSettings
        {
            get
            {
                // TODO: consider caching globally
                if (this.siteSettings == null)
                {
                    this.siteSettings = new SiteSettings(Home.GetCurrentHome(this));
                }

                return this.siteSettings;
            }
        }

        /// <summary>
        /// returns true if the sitePage supplied is either the current page, or an ancestor of the current page, (ie. it's in the path of this)
        /// </summary>
        /// <param name="sitePage">the page to test to see if it's in the current path</param>
        public bool IsInPathOf(SitePage sitePage)
        {
            return this.AncestorsOrSelf().Select(x => x.Id).Contains(sitePage.Id);
        }
    }
}
