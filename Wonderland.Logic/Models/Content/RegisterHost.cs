
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterHost : SitePage
    {
        // Document Type
        public new const string Alias = "RegisterHost";

        // Properties
        public const string MarketingSourcesAlias = "marketingSources";
        public const string TShirtSizesAlias = "tShirtSizes";

        public RegisterHost(IPublishedContent content)
            : base(content)
        {
        }

        public IEnumerable<string> MarketingSources
        {
            get
            {
                return this.GetPropertyValue<string[]>(RegisterHost.MarketingSourcesAlias);
            }
        }

        public IEnumerable<string> TShirtSizes
        {
            get
            {
                return this.GetPropertyValue<string[]>(RegisterHost.TShirtSizesAlias);
            }
        }
    }
}
