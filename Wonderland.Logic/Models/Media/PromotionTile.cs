
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
        public new const string Alias = "PromotionTile";

        public PromotionTile(IPublishedContent content)
            : base(content)
        {
        }

        public string TileImage
        {
            get
            {
                return "tileImage";
            }
        }

        public string OverlayImage
        {
            get
            {
                return "overlayImage";
            }
        }
    }
}
