
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Members;

    public class RegisterGuest : SitePage
    {
        // Document Type
        public new const string Alias = "RegisterGuest";

        public RegisterGuest(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// set by controller
        /// </summary>
        public PartyHost PartyHost
        {
            get;
            internal set;
        }
    }
}
