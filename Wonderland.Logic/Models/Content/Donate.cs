
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Members;

    public class Donate : SitePage
    {
        // Document Type
        public new const string Alias = "Donate";

        public Donate(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// set by the controller to identify the host member for this renderModel
        /// </summary>
        public PartyHost PartyHost
        {
            get;
            internal set;
        }
    }
}
