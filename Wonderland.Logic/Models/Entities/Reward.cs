
namespace Wonderland.Logic.Models.Entities
{
    using Wonderland.Logic.Models.Media;
    using Archetype.Models;
    using Umbraco.Core.Models;

    public class Reward
    {
        internal Reward(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Heading = archetypeFieldsetModel.GetValue<string>("heading");
            this.Image = new Image(archetypeFieldsetModel.GetValue<IPublishedContent>("image"));
            this.SubHeading = archetypeFieldsetModel.GetValue<string>("subHeading");
            this.Copy = archetypeFieldsetModel.GetValue<string>("copy");
        }

        public string Heading { get; internal set; }

        public Image Image { get; internal set; }

        public string SubHeading { get; internal set; }

        public string Copy { get; internal set; }
    }
}
