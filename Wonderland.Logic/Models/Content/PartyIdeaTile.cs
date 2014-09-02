
namespace Wonderland.Logic.Models.Content
{
    using nuPickers;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Media;

    public abstract class PartyIdeaTile : SitePage
    {
        // DocumentType
        public new const string Alias = "PartyIdeaTile";

        // Properties
        public const string TileHeadingAlias = "tileHeading";
        public const string TileSizeAlias = "tileSize";
        public const string TileImageAlias = "tileImage";
        public const string TileCopyAlias = "tileCopy";

        public PartyIdeaTile(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// string used to build a css class name
        /// </summary>
        public abstract string TileType
        {
            get;
        }

        public string TileHeading
        {
            get
            {
                return this.GetPropertyValue<string>(PartyIdeaTile.TileHeadingAlias);
            }
        }

        public PartyIdeaTileSize TileSize
        {
            get
            {
                return this.GetPropertyValue<Picker>(PartyIdeaTile.TileSizeAlias).AsEnums<PartyIdeaTileSize>().Single();
            }
        }

        public Image TileImage
        {
            get
            {
                return new Image(this.GetPropertyValue<IPublishedContent>(PartyIdeaTile.TileImageAlias));
            }
        }

        public string TileCopy
        {
            get
            {
                return this.GetPropertyValue<string>(PartyIdeaTile.TileCopyAlias);
            }
        }
    }
}
