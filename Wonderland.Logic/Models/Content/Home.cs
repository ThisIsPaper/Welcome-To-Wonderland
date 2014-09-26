
namespace Wonderland.Logic.Models.Content
{
    using Archetype.Models;
    using nuPickers;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;

    public class Home : SitePage
    {
        // Document Type
        public new const string Alias = "Home";

        // Properties
        public const string BannerHeadingAlias = "bannerHeading";
        public const string BannerCopyAlias = "bannerCopy";
        public const string PromotionTilesAlias = "promotionTiles";
        public const string HeaderNavigationAlias = "headerNavigation";
        public const string FooterNavigationAlias = "footerNavigation";
        public const string LegalNavigationAlias = "legalNavigation";
        public const string CampaignHashtagAlias = "campaignHashtag";
        public const string FacebookUrlAlias = "facebookUrl";
        public const string TwitterUrlAlias = "twitterUrl";
        public const string GooglePlusUrlAlias = "googlePlusUrl";
        public const string FooterCopyAlias = "footerCopy";

        public Home(IPublishedContent content) : base(content)
        {
        }

        public string BannerHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Home.BannerHeadingAlias);
            }
        }

        public string BannerCopy
        {
            get
            {
                return this.GetPropertyValue<string>(Home.BannerCopyAlias);
            }
        }

        public IEnumerable<PromotionTile> PromotionTiles
        {
            get
            {
                return this.GetPropertyValue<ArchetypeModel>(Home.PromotionTilesAlias).Select(x => new PromotionTile(x));
            }
        }

        /// <summary>
        /// set in hijack render controller so as to cache the calculation
        /// </summary>
        public decimal TotalAmountRaised
        {
            get; 
            internal set;
        }

        internal IEnumerable<SitePage> HeaderNavigation
        {
            get
            {
                return this.GetPropertyValue<Picker>(Home.HeaderNavigationAlias).AsPublishedContent().Select(x => new SitePage(x));
            }
        }

        internal IEnumerable<SitePage> FooterNavigation
        {
            get
            {
                return this.GetPropertyValue<Picker>(Home.FooterNavigationAlias).AsPublishedContent().Select(x => new SitePage(x));
            }
        }

        internal IEnumerable<SitePage> LegalNavigation
        {
            get
            {
                return this.GetPropertyValue<Picker>(Home.LegalNavigationAlias).AsPublishedContent().Select(x => new SitePage(x));
            }
        }

        internal string CampaignHashtag
        {
            get
            {
                return this.GetPropertyValue<string>(Home.CampaignHashtagAlias);
            }
        }

        internal string FacebookUrl
        {
            get
            {
                return this.GetPropertyValue<string>(Home.FacebookUrlAlias);
            }
        }

        internal string TwitterUrl
        {
            get
            {
                return this.GetPropertyValue<string>(Home.TwitterUrlAlias);
            }
        }

        internal string GooglePlusUrl
        {
            get
            {
                return this.GetPropertyValue<string>(Home.GooglePlusUrlAlias);
            }
        }

        internal string FooterCopy
        {
            get
            {
                return this.GetPropertyValue<string>(Home.FooterCopyAlias);
            }
        }

        /// <summary>
        /// helper that walks up the tree to get the current home
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        internal static Home GetCurrentHome(IPublishedContent content)
        {
            return new Home(content.AncestorOrSelf(Home.Alias));
        }
    }
}
