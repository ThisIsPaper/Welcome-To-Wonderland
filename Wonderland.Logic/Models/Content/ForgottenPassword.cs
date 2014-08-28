
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class ForgottenPassword : SitePage
    {
        public new const string Alias = "ForgottenPassword";

        public ForgottenPassword(IPublishedContent content)
            : base(content)
        {
            this.ShowHeaderAndFooter = false;
        }
    }
}
