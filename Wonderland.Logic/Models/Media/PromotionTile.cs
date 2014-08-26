
namespace Wonderland.Logic.Models.Media
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Umbraco.Core.Models.PublishedContent;

    public class PromotionTile : PublishedContentModel
    {
        public const string Alias = "PromotionTile";

        public PromotionTile(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// returns the url for the square crop (this is the only crop)
        /// </summary>
        public string TileImageUrl
        {
            get
            {
                return this.GetCropUrl("tileImage", "Square");                
            }
        }

        /// <summary>
        /// returns the uploaded image (this will be hand-crafted, hence will have exact dimensions)
        /// </summary>
        public string OverlayImageUrl
        {
            get
            {
                return this.GetPropertyValue<string>("overlayImage");
            }
        }
    }
}
