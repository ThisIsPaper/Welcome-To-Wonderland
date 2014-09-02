
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
        // DocumentType
        public new const string Alias = "PistePoints";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string RewardsAlias = "rewards";
        public const string RegisterHostLinkTextAlias = "registerHostLinkText";
        public const string PartyKitAlias = "partyKit";

        public PistePoints(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get 
            { 
                return this.GetPropertyValue<string>(PistePoints.PageHeadingAlias);
            }
        }

        public IEnumerable<Reward> Rewards
        {
            get
            {
                return this.GetPropertyValue<ArchetypeModel>(PistePoints.RewardsAlias).Select(x => new Reward(x));
            }
        }

        public string RegisterHostLinkText
        {
            get
            {
                return this.GetPropertyValue<string>(PistePoints.RegisterHostLinkTextAlias);
            }
        }

        public PartyKit PartyKit
        {
            get
            {
                return new PartyKit(this.GetPropertyValue<ArchetypeModel>(PistePoints.PartyKitAlias).Single());
            }
        }        
    }
}
