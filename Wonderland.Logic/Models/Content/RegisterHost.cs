
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterHost : SitePage
    {
        public new const string Alias = "RegisterHost";

        public RegisterHost(IPublishedContent content)
            : base(content)
        {
        }

        public IEnumerable<string> MarketingSources
        {
            get
            {
                return this.GetPropertyValue<string[]>("marketingSources");
            }
        }
    }
}
