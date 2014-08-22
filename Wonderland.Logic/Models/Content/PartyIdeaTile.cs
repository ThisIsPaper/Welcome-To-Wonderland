
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Wonderland.Logic.Enums;

    public class PartyIdeaTile : SitePage
    {
        public const string Alias = "PartyIdeaTile";

        public PartyIdeaTile(IPublishedContent content)
            : base(content)
        {
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

        // IMAGE 

        public string TileCopy
        {
            get
            {
                return this.GetPropertyValue<string>("tileCopy");
            }
        }
    }
}
