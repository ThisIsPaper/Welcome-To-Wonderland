
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterHostPartyKit : SitePage
    {
        public new const string Alias = "RegisterHostPartyKit";

        public RegisterHostPartyKit(IPublishedContent content)
            : base(content)
        {
            this.ShowHeaderAndFooter = false;
        }
    }
}
