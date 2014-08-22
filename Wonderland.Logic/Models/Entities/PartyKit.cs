
namespace Wonderland.Logic.Models.Entities
{
    using System.Linq;
    using System.Collections.Generic;
    using Wonderland.Logic.Models.Media;
    using Archetype.Models;
    using Umbraco.Core.Models;
    using System.Web.WebPages;

    public class PartyKit
    {
        internal PartyKit(ArchetypeFieldsetModel archetypeFieldsetModel)
        {
            this.Heading = archetypeFieldsetModel.GetValue<string>("heading");                        
            //this.Images = archetypeFieldsetModel.GetValue<IEnumerable<IPublishedContent>>("images").Select(x => new Image(x));
            this.LinkText = archetypeFieldsetModel.GetValue<string>("linkText");
            this.Copy = archetypeFieldsetModel.GetValue<string>("copy");
        }

        public string Heading { get; internal set; }

        public IEnumerable<Image> Images { get; internal set;}

        public string LinkText { get; internal set; }

        public string Copy { get; internal set; }
    }
}
