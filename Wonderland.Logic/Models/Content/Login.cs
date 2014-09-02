
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class Login : SitePage
    {
        // DocumentType
        public new const string Alias = "Login";

        // Properties
        public const string PageHeadingAlias = "pageHeading";

        public Login(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(Login.PageHeadingAlias);
            }
        }
    }
}
