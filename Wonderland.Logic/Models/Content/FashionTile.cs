
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class FashionTile : PartyIdeaTile
    {
        // Document Type
        public new const string Alias = "FashionTile";

        // Properties
        public const string PinterestBoardUrlAlias = "pinterestBoardUrl";

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

        public string PinterestBoardUrl
        {
            get
            {
                return this.GetPropertyValue<string>(FashionTile.PinterestBoardUrlAlias);
            }
        }
    }
}
