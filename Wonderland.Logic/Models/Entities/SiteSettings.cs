
namespace Wonderland.Logic.Models.Entities
{
    using System.Linq;
    using System.Collections.Generic;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Media;

    public class SiteSettings
    {
        internal SiteSettings(Home home)
        {
            this.HeaderNavigation = home.HeaderNavigation;
            this.HeaderNavigationLeft = this.HeaderNavigation.Take(home.HeaderNavigation.Count() / 2);
            this.HeaderNavigationRight = this.HeaderNavigation.Skip(home.HeaderNavigation.Count() / 2);

            this.FooterNavigation = home.FooterNavigation;
            this.LegalNavigation = home.LegalNavigation;

            this.MetaOgSiteName = home.MetaOgSiteName;
            this.DefaultMetaOgTitle = home.DefaultMetaOgTitle;
            this.DefaultMetaOgImage = home.DefaultMetaOgImage;

            this.RegisterHostUrl = home.Children.Single(x => x.DocumentTypeAlias == RegisterHost.Alias).Url;
            this.LoginUrl = home.Children.Single(x => x.DocumentTypeAlias == Login.Alias).Url;
            this.ProfileUrl = home.Children.Single(x => x.DocumentTypeAlias == Profile.Alias).Url;

            this.CampaignHashtag = home.CampaignHashtag;
            this.FacebookUrl = home.FacebookUrl;
            this.TwitterUrl = home.TwitterUrl;
            this.GooglePlusUrl = home.GooglePlusUrl;
            this.FooterCopy = home.FooterCopy;
        }

        public IEnumerable<SitePage> HeaderNavigation { get; private set; }

        public IEnumerable<SitePage> HeaderNavigationLeft { get; private set; }

        public IEnumerable<SitePage> HeaderNavigationRight { get; private set; }

        public IEnumerable<SitePage> FooterNavigation { get; private set; }

        public IEnumerable<SitePage> LegalNavigation { get; private set; }

        public string MetaOgSiteName { get; private set; }

        public string DefaultMetaOgTitle { get; private set; }

        public Image DefaultMetaOgImage { get; private set; }

        public string RegisterHostUrl { get; private set; }

        public string LoginUrl { get; private set; }

        public string ProfileUrl { get; private set; }

        public string CampaignHashtag { get; private set; }

        public string FacebookUrl { get; private set; }

        public string TwitterUrl { get; private set; }

        public string GooglePlusUrl { get; private set; }

        public string FooterCopy { get; private set; }

        public static int CacheMinutes { get { return 5; } }
    }
}
