﻿
namespace Wonderland.Logic.Models.Entities
{
    using System.Collections.Generic;
    using Wonderland.Logic.Models.Content;

    public class SiteSettings
    {
        internal SiteSettings(Home home)
        {
            this.HeaderNavigation = home.HeaderNavigation;
            this.FooterNavigation = home.FooterNavigation;
            this.LegalNavigation = home.LegalNavigation;
            this.CampaignHashtag = home.CampaignHashtag;
            this.FacebookUrl = home.FacebookUrl;
            this.TwitterUrl = home.TwitterUrl;
            this.GooglePlusUrl = home.GooglePlusUrl;
            this.FooterCopy = home.FooterCopy;
        }

        public IEnumerable<SitePage> HeaderNavigation { get; private set; }

        public IEnumerable<SitePage> FooterNavigation { get; private set; }

        public IEnumerable<SitePage> LegalNavigation { get; private set; }

        public string CampaignHashtag { get; private set; }

        public string FacebookUrl { get; private set; }

        public string TwitterUrl { get; private set; }

        public string GooglePlusUrl { get; private set; }

        public string FooterCopy { get; private set; }
    }
}