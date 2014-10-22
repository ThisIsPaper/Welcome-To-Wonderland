
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Wonderland.Logic.Interfaces;

    public class PartyImage : NonSerializablePublishedContentModel, IPartyImage
    {
        // Media Type
        public const string Alias = "PartyImage";

        public PartyImage(IPublishedContent content)
            : base(content)
        {

        }
    }
}
