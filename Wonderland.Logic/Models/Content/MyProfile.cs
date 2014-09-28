
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class MyProfile : SitePage
    {
        // Document Type
        public new const string Alias = "MyProfile";

        public MyProfile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
