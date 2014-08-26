
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Umbraco.Core.Models.PublishedContent;

    public class PartyIdeas : SitePage
    {
        public new const string Alias = "PartyIdeas";

        public PartyIdeas(IPublishedContent content)
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

        public IEnumerable<PartyIdeaTile> GetPartyIdeaTiles()
        {
            // TODO: add cms field to allow overall ordering amongst tile types

            // reuse the built-in IPublishedContent -> Model factory
            return this.Descendants()
                        .Where(x => 
                                x.DocumentTypeAlias == DecorationTile.Alias ||
                                x.DocumentTypeAlias == FashionTile.Alias ||
                                x.DocumentTypeAlias == PlaylistTile.Alias ||
                                x.DocumentTypeAlias == RecipeTile.Alias)
                        .Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));
        }
    }
}
