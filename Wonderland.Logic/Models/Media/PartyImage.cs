
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class PartyImage : NonSerializablePublishedContentModel // PublishedContentModel
    {
        // Media Type
        public const string Alias = "PartyImage";

        public PartyImage(IPublishedContent content)
            : base(content)
        {

        }
    }
}
