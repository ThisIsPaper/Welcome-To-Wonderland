
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Members;
    using Wonderland.Logic.Models.Database;

    public class Donate : SitePage
    {
        // Document Type
        public new const string Alias = "Donate";

        // Properties
        public const string ServerEmailAddressAlias = "serverEmailAddress";
        public const string EmailSubjectAlias = "emailSubject";
        public const string EmailBodyAlias = "emailBody";

        public Donate(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// set by the controller to identify the host member for this renderModel
        /// </summary>
        public PartyHost PartyHost
        {
            get;
            internal set;
        }

        /// <summary>
        /// after a successfull transaction, this property is populated
        /// </summary>
        public DonationRow DonationRow
        {
            get;
            internal set;
        }

        public string ServerEmailAddress
        {
            get
            {
                return this.GetPropertyValue<string>(Donate.ServerEmailAddressAlias);
            }
        }

        public string EmailSubject
        {
            get
            {
                return this.GetPropertyValue<string>(Donate.EmailSubjectAlias);
            }
        }

        public string EmailBody
        {
            get
            {
                return this.GetPropertyValue<string>(Donate.EmailBodyAlias);
            }
        }

    }
}
