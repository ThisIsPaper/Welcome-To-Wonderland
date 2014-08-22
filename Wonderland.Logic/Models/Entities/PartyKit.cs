
namespace Wonderland.Logic.Models.Entities
{
    using Archetype.Models;

    public class PartyKit
    {
        internal PartyKit(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Heading = archetypeFieldsetModel.GetValue<string>("heading");

            this.LinkText = archetypeFieldsetModel.GetValue<string>("linkText");
            this.Copy = archetypeFieldsetModel.GetValue<string>("copy");
        }

        public string Heading { get; internal set; }

        // Images

        public string LinkText { get; internal set; }

        public string Copy { get; internal set; }
    }
}
