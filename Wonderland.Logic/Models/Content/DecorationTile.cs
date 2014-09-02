
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class DecorationTile : PartyIdeaTile
    {
        // Document Type
        public new const string Alias = "DecorationTile";

        // Properties
        public const string PinterestBoardUrlAlias = "pinterestBoardUrl";

        public DecorationTile(IPublishedContent content)
            : base(content)
        {
        }

        public override string TileType
        {
            get 
            { 
                return "decoration"; 
            }
        }

        public string PinterestBoradUrl
        {
            get
            {
                return this.GetPropertyValue<string>(DecorationTile.PinterestBoardUrlAlias);
            }
        }
    }
}
