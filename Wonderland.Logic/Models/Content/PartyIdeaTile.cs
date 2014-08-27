
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
        public new const string Alias = "PartyIdeaTile";

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
                return this.GetPropertyValue<string>("tileHeading");
            }
        }

        public PartyIdeaTileSize TileSize
        {
            get
            {
                return this.GetPropertyValue<Picker>("tileSize").AsEnums<PartyIdeaTileSize>().Single();
            }
        }

        public Image TileImage
        {
            get
            {
                return new Image(this.GetPropertyValue<IPublishedContent>("tileImage"));
            }
        }

        public string TileCopy
        {
            get
            {
                return this.GetPropertyValue<string>("tileCopy");
            }
        }
    }
}
