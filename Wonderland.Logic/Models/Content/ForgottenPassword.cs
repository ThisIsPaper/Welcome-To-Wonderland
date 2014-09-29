
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
        public const string ServerEmailAddressAlias = "serverEmailAddress";
        public const string EmailSubjectAlias = "emailSubject";
        public const string EmailBodyAlias = "emailBody";

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

        public string ServerEmailAddress
        {
            get
            {
                return this.GetPropertyValue<string>(ForgottenPassword.ServerEmailAddressAlias);
            }
        }

        public string EmailSubject
        {
            get
            {
                return this.GetPropertyValue<string>(ForgottenPassword.EmailSubjectAlias);
            }
        }

        public string EmailBody
        {
            get
            {
                return this.GetPropertyValue<string>(ForgottenPassword.EmailBodyAlias);
            }
        }
    }
}
