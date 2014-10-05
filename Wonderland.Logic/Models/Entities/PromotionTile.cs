
namespace Wonderland.Logic.Models.Entities
{
    using System.Linq;
    using Archetype.Models;
    using Umbraco.Core.Models;
    using Wonderland.Logic.Models.Media;
    using RJP.MultiUrlPicker.Models;

    public class PromotionTile
    {
        internal PromotionTile(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Image = new Image(archetypeFieldsetModel.GetValue<IPublishedContent>("image"));
            this.OverlayImage = new Image(archetypeFieldsetModel.GetValue<IPublishedContent>("overlayImage"));
            this.AltText = archetypeFieldsetModel.GetValue<string>("altText");
            this.Link = archetypeFieldsetModel.GetValue<MultiUrls>("link").Single().Url;
        }

        public Image Image { get; internal set; }

        public Image OverlayImage { get; internal set; }

        public string AltText { get; internal set; }

        public string Link { get; internal set; }
    }
}
