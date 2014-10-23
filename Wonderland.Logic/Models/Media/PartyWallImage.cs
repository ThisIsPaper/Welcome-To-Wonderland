
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class PartyWallImage : SerializablePublishedContentModel
    {
        // Media Type
        public const string Alias = "PartyWallImage";


        public PartyWallImage(IPublishedContent content)
            : base(content)
        {

        }
    }
}
