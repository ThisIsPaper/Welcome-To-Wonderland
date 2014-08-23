
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

        public string TileImageUrl
        {
            get
            {
                return this.GetPropertyValue<string>("tileImage");
            }
        }

        public string OverlayImageUrl
        {
            get
            {
                return this.GetPropertyValue<string>("overlayImage");
            }
        }
    }
}
