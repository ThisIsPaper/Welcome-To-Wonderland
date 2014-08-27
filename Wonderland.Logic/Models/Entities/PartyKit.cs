
namespace Wonderland.Logic.Models.Entities
{
    using Archetype.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Wonderland.Logic.Models.Media;

    public class PartyKit
    {
        internal PartyKit(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Heading = archetypeFieldsetModel.GetValue<string>("heading");
            this.Images = archetypeFieldsetModel.GetValue<IEnumerable<IPublishedContent>>("images", Enumerable.Empty<IPublishedContent>()).Select(x => new Image(x));
            this.LinkText = archetypeFieldsetModel.GetValue<string>("linkText");
            this.Copy = archetypeFieldsetModel.GetValue<string>("copy");
        }

        public string Heading { get; internal set; }

        public IEnumerable<Image> Images { get; internal set;}

        public string LinkText { get; internal set; }

        public string Copy { get; internal set; }
    }
}
