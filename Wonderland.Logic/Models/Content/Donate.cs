
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Donate : SitePage
    {
        // Document Type
        public new const string Alias = "Donate";

        public Donate(IPublishedContent content)
            : base(content)
        {
        }
    }
}
