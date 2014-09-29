
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class ResetPassword : SitePage
    {
        // Document Type
        public new const string Alias = "ResetPassword";
        
        // Properties
        public const string PageHeadingAlias = "pageHeading";

        public ResetPassword(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(ResetPassword.PageHeadingAlias);
            }
        }
    }
}
