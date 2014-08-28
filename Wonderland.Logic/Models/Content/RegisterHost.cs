
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterHost : SitePage
    {
        public new const string Alias = "RegisterHost";

        public RegisterHost(IPublishedContent content)
            : base(content)
        {
            this.ShowHeaderAndFooter = false;
        }
    }
}
