
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class PlaylistTile : PartyIdeaTile
    {
        // Document Type
        public new const string Alias = "PlaylistTile";

        // Properties
        public const string PlaylistNameAlias = "playlistName";
        public const string SpotifyUrlAlias = "spotifyUrl";

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

        public string PlaylistName
        {
            get
            {
                return this.GetPropertyValue<string>(PlaylistTile.PlaylistNameAlias);
            }
        }

        public string SpotifyUrl
        {
            get
            {
                return this.GetPropertyValue<string>(PlaylistTile.SpotifyUrlAlias);
            }
        }
    }
}
