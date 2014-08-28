
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
            this.ShowHeaderAndFooter = false;
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>("pageHeading");
            }
        }
    }
}
