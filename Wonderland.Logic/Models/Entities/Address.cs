
namespace Wonderland.Logic.Models.Entities
{
    using Archetype.Models;

    public class Address
    {
        internal Address(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Address1 = archetypeFieldsetModel.GetValue<string>("address1");
            this.Address2 = archetypeFieldsetModel.GetValue<string>("address2");
            this.TownCity = archetypeFieldsetModel.GetValue<string>("townCity");
            this.Postcode = archetypeFieldsetModel.GetValue<string>("postcode");
        }

        public string Address1 { get; internal set; }

        public string Address2 { get; internal set; }

        public string TownCity { get; internal set; }

        public string Postcode { get; internal set; }
    }
}
