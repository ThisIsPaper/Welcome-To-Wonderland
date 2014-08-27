
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;
    using Umbraco.Web;

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

        public string PlaylistName
        {
            get
            {
                return this.GetPropertyValue<string>("playlistName");
            }
        }

        public string SpotifyUrl
        {
            get
            {
                return this.GetPropertyValue<string>("spotifyUrl");
            }
        }
    }
}
