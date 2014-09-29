
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Profile : SitePage
    {
        // Document Type
        public new const string Alias = "Profile";

        public Profile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
