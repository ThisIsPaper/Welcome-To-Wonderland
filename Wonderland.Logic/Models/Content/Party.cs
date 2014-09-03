
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Members;

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

        /// <summary>
        /// set by the controller to identify the host member for this party
        /// </summary>
        public PartyHost PartyHost
        {
            get;
            internal set;
        }
    }
}
