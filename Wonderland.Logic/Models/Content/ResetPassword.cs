
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class ResetPassword : SitePage
    {
        public new const string Alias = "ResetPassword";

        public ResetPassword(IPublishedContent content)
            : base(content)
        {
        }
    }
}
