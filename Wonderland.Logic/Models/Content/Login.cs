
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Login : SitePage
    {
        public new const string Alias = "Login";

        public Login(IPublishedContent content)
            : base(content)
        {
        }
    }
}
