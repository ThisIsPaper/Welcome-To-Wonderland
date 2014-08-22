
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Archetype.Models;
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

        public string HostAPartyLinkText
        {
            get
            {
                return this.GetPropertyValue<string>("hostAPartyLinkText");
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
