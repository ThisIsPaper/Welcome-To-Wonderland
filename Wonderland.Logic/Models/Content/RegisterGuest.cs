
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RegisterGuest : SitePage
    {
        public new const string Alias = "RegisterGuest";

        public RegisterGuest(IPublishedContent content)
            : base(content)
        {
        }
    }
}
