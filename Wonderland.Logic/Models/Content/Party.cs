
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Party : SitePage
    {
        // DocumentType
        public new const string Alias = "Party";

        // Properties
        public const string DefaultHeadingAlias = "defaultHeading";
        public const string DefaultCopyAlias = "defaultCopy";

        public Party(IPublishedContent content)
            : base(content)
        {
        }

        public string DefaultHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Party.DefaultHeadingAlias);
            }
        }

        public string DefaultCopy
        {
            get
            {
                return this.GetPropertyValue<string>(Party.DefaultCopyAlias);
            }
        }
    }
}
