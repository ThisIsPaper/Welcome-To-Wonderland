
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public abstract class PartyIdeaCategory : SitePage
    {
        // DocumentType
        public new const string Alias = "PartyIdeaCategory";

        public PartyIdeaCategory(IPublishedContent content)
            : base(content)
        {
        }

        //public IEnumerable<PartyIdeaTile> GetPartyIdeaTiles()
        //{
        //    return this.Children().Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));
        //}
    }
}
