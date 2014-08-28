
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterHostPartyUrl : SitePage
    {
        public new const string Alias = "RegisterHostPartyUrl";

        public RegisterHostPartyUrl(IPublishedContent content)
            : base(content)
        {
        }
    }
}
