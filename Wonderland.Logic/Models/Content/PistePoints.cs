
namespace Wonderland.Logic.Models.Content
{
    using Archetype.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;

    public class PistePoints : SitePage
    {
        public new const string Alias = "PistePoints";

        public PistePoints(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get 
            { 
                return this.GetPropertyValue<string>("pageHeading");
            }
        }

        public IEnumerable<Reward> Rewards
        {
            get
            {
                return this.GetPropertyValue<ArchetypeModel>("rewards").Select(x => new Reward(x));
            }
        }

        public string RegisterHostLinkText
        {
            get
            {
                return this.GetPropertyValue<string>("registerHostLinkText");
            }
        }

        public PartyKit PartyKit
        {
            get
            {
                return new PartyKit(this.GetPropertyValue<ArchetypeModel>("partyKit").Single());
            }
        }        
    }
}
