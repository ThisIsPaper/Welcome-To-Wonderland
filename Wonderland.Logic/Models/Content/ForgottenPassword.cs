
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class ForgottenPassword : SitePage
    {
        // Document Type
        public new const string Alias = "ForgottenPassword";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string ForgottenPasswordEmailAlias = "forgottenPasswordEmail";

        public ForgottenPassword(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(ForgottenPassword.PageHeadingAlias);
            }
        }

        public string ForgottenPasswordEmail
        {
            get
            {
                return this.GetPropertyValue<string>(ForgottenPassword.ForgottenPasswordEmailAlias);
            }
        }
    }
}
