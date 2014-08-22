
namespace Wonderland.Logic.Models.Entities
{
    using Archetype.Models;

    public class Reward
    {
        internal Reward(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Heading = archetypeFieldsetModel.GetValue<string>("heading");
            this.SubHeading = archetypeFieldsetModel.GetValue<string>("subHeading");
            this.Copy = archetypeFieldsetModel.GetValue<string>("copy");
        }

        public string Heading { get; internal set; }

        // Image

        public string SubHeading { get; internal set; }

        public string Copy { get; internal set; }
    }
}
