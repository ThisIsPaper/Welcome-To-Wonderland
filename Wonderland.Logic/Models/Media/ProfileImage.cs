
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class ProfileImage : NonSerializablePublishedContentModel
    {
        // Media Type
        public const string Alias = "ProfileImage";


        public ProfileImage(IPublishedContent content)
            : base(content)
        {

        }
    }
}
