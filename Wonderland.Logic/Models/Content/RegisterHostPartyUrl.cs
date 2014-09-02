
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterHostPartyUrl : SitePage
    {
        // Document Type
        public new const string Alias = "RegisterHostPartyUrl";

        public RegisterHostPartyUrl(IPublishedContent content)
            : base(content)
        {
        }
    }
}
