
namespace Wonderland.Logic.Models.Content
{
    using Archetype.Models;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;

    public class FundraisingIdeas : SitePage
    {
        public new const string Alias = "FundraisingIdeas";

        public FundraisingIdeas(IPublishedContent content)
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

        public PartyKit PartyKit
        {
            get
            {
                return new PartyKit(this.GetPropertyValue<ArchetypeModel>("partyKit").Single());
            }
        }        

        public string SubHeading
        {
            get
            {
                return this.GetPropertyValue<string>("subHeading");
            }
        }


        public IEnumerable<FundraisingIdea> GetFundraisingIdeas()
        {
            return this.Children().Select(x => new FundraisingIdea(x));
        }
    }
}
