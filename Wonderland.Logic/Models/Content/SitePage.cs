
namespace Wonderland.Logic.Models.Content
{
    using System.Linq;
    using System.Web;
    using System.Web.Caching;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Media;

    public class SitePage : BaseContentType
    {
        public const int CacheDuration = 3600;

        // Document Type
        public const string Alias = "SitePage";

        // Properties
        public const string MetaDescriptionAlias = "metaDescription";
        public const string MetaKeywordsAlias = "metaKeywords";
        public const string MetaOgTitleAlias = "metaOgTitle";

        public SitePage(IPublishedContent content) : base(content)
        {
        }

        public string MetaDescription
        {
            get
            {
                return this.GetPropertyValue<string>(SitePage.MetaDescriptionAlias);
            }
        }

        public string MetaKeywords
        {
            get
            {
                return this.GetPropertyValue<string>(SitePage.MetaKeywordsAlias);
            }
        }

        public virtual string MetaOgTitle
        {
            get
            {
                string metaOgTitle = this.GetPropertyValue<string>(SitePage.MetaOgTitleAlias);

                if (string.IsNullOrWhiteSpace(metaOgTitle))
                {
                    metaOgTitle = this.SiteSettings.DefaultMetaOgTitle;
                }

                return metaOgTitle;
            }
        }

        public virtual string MetaOgDescription
        {
            get
            {
                return this.MetaDescription;
            }
        }

        public virtual Image MetaOgImage
        {
            get
            {
                return this.SiteSettings.DefaultMetaOgImage;
            }
        }

        public string DomainUrl
        {
            get
            {
                return Home.GetCurrentHome(this).UrlWithDomain().TrimEnd('/');
            }
        }

        public SiteSettings SiteSettings
        {
            get
            {
                SiteSettings siteSettings = null;

                Cache cache = HttpContext.Current.Cache;

                siteSettings = (SiteSettings)cache["SiteSettings"];

                if (siteSettings == null)
                {
                    siteSettings = new SiteSettings(Home.GetCurrentHome(this));
                    cache.Insert("SiteSettings", siteSettings);
                }

                return siteSettings;
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
