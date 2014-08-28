
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterGuestConfirmation : SitePage
    {
        public new const string Alias = "RegisterGuestConfirmation";

        public RegisterGuestConfirmation(IPublishedContent content)
            : base(content)
        {
        }
    }
}
