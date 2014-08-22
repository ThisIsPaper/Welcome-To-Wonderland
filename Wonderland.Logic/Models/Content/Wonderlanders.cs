
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Wonderlanders : SitePage
    {
        public new const string Alias = "Wonderlanders";

        public Wonderlanders(IPublishedContent content)
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

        public string TopFundraisersHeading
        {
            get
            {
                return this.GetPropertyValue<string>("topFundraisersHeading");
            }
        }

        public int TopFundraisersCount
        {
            get
            {
                return this.GetPropertyValue<int>("topFundraisersCount");
            }
        }

        public string MostGuestsHeading
        {
            get
            {
                return this.GetPropertyValue<string>("mostGuestsHeading");
            }
        }

        public int MostGuestsCount
        {
            get
            {
                return this.GetPropertyValue<int>("mostGuestsCount");
            }
        }

        public string BadgesTodayHeading
        {
            get
            {
                return this.GetPropertyValue<string>("badgesTodayHeading");
            }
        }

        public string MostRecentHostsHeading
        {
            get
            {
                return this.GetPropertyValue<string>("mostRecentHostsHeading");
            }
        }

        public int MostRecentHostsCount
        {
            get
            {
                return this.GetPropertyValue<int>("mostRecentHostsCount");
            }
        }
    }
}
