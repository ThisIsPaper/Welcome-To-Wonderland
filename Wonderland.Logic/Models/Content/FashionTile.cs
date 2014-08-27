
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

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

        public string PinterestBoradUrl
        {
            get
            {
                return this.GetPropertyValue<string>("pinterestBoardUrl");
            }
        }
    }
}
