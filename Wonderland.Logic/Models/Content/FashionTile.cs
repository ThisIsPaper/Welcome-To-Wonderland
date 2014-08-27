
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class FashionTile : PartyIdeaTile
    {
        public new const string Alias = "FashionTile";

        public FashionTile(IPublishedContent content)
            : base(content)
        {
        }

        public override string TileType
        {
            get
            {
                return "fashion";
            }
        }

    }
}
