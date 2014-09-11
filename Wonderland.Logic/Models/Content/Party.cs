
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Members;
    using System.Collections.Generic;
    
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
            this.Heading = this.DefaultHeading;
            this.Copy = this.DefaultCopy;
        }

        private string DefaultHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Party.DefaultHeadingAlias);
            }
        }

        private string DefaultCopy
        {
            get
            {
                return this.GetPropertyValue<string>(Party.DefaultCopyAlias);
            }
        }

        /// <summary>
        /// set by the controller to identify the host member for this renderModel
        /// </summary>
        public PartyHost PartyHost
        {
            get;
            internal set;
        }

        public string Heading
        {
            get;
            internal set;
        }

        public string Copy
        {
            get;
            internal set;
        }

        public IEnumerable<IPartier> Partiers
        {
            get;
            internal set;
        }
    }
}
