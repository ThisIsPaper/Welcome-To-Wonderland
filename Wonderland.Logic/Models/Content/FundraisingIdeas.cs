
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
        // Document Type
        public new const string Alias = "FundraisingIdeas";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string PartyKitAlias = "partyKit";
        public const string SubHeadingAlias = "subHeading";


        public FundraisingIdeas(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(FundraisingIdeas.PageHeadingAlias);
            }
        }

        public PartyKit PartyKit
        {
            get
            {
                return new PartyKit(this.GetPropertyValue<ArchetypeModel>(FundraisingIdeas.PartyKitAlias).Single());
            }
        }        

        public string SubHeading
        {
            get
            {
                return this.GetPropertyValue<string>(FundraisingIdeas.SubHeadingAlias);
            }
        }


        public IEnumerable<FundraisingIdea> GetFundraisingIdeas()
        {
            return this.Children().Select(x => new FundraisingIdea(x));
        }
    }
}
