
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Home : SitePage
    {
        public const string Alias = "Home";

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

        internal IEnumerable<SitePage> HeaderNavigation
        {
            get
            {
                return this.GetPropertyValue<Picker>("headerNavigation").AsPublishedContent().Select(x => new SitePage(x));
            }
        }

        internal IEnumerable<SitePage> FooterNavigation
        {
            get
            {
                return this.GetPropertyValue<Picker>("footerNavigation").AsPublishedContent().Select(x => new SitePage(x));
            }
        }

        internal IEnumerable<SitePage> LegalNavigation
        {
            get
            {
                return this.GetPropertyValue<Picker>("legalNavigation").AsPublishedContent().Select(x => new SitePage(x));
            }
        }

        internal string CampaignHashtag
        {
            get
            {
                return this.GetPropertyValue<string>("campaignHashtag");
            }
        }

        internal string FacebookUrl
        {
            get
            {
                return this.GetPropertyValue<string>("facebookUrl");
            }
        }

        internal string TwitterUrl
        {
            get
            {
                return this.GetPropertyValue<string>("twitterUrl");
            }
        }

        internal string GooglePlusUrl
        {
            get
            {
                return this.GetPropertyValue<string>("googlePlusUrl");
            }
        }

        internal string FooterCopy
        {
            get
            {
                return this.GetPropertyValue<string>("footerCopy");
            }
        }
    }
}
