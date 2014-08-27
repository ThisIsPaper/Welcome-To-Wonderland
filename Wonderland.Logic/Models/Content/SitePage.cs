
namespace Wonderland.Logic.Models.Content
{
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;

    public class SitePage : PublishedContentModel
    {
        public const string Alias = "SitePage";

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
