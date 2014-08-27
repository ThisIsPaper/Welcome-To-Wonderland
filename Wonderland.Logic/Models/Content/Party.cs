
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Party : SitePage
    {
        public new const string Alias = "Party";

        public Party(IPublishedContent content)
            : base(content)
        {
        }

        public string DefaultHeading
        {
            get
            {
                return this.GetPropertyValue<string>("defaultHeading");
            }
        }

        public string DefaultCopy
        {
            get
            {
                return this.GetPropertyValue<string>("defaultCopy");
            }
        }
    }
}
