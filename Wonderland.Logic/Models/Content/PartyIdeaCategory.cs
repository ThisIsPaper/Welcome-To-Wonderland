
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using nuPickers;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Media;

    public abstract class PartyIdeaCategory : SitePage
    {
        public new const string Alias = "PartyIdeaCategory";

        public PartyIdeaCategory(IPublishedContent content)
            : base(content)
        {
        }

        public IEnumerable<PartyIdeaTile> GetPartyIdeaTiles()
        {
            return this.Children().Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));
        }
    }
}
