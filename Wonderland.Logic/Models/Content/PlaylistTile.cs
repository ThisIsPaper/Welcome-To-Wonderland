
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class PlaylistTile : PartyIdeaTile
    {
        public new const string Alias = "PlaylistTile";

        public PlaylistTile(IPublishedContent content)
            : base(content)
        {
        }

        public override string TileType
        {
            get
            {
                return "playlist";
            }
        }
    }
}
