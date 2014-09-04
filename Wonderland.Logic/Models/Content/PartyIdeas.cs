
namespace Wonderland.Logic.Models.Content
{
    using nuPickers;
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class PartyIdeas : SitePage
    {
        // DocumentType
        public new const string Alias = "PartyIdeas";

        // Properties
        public const string PageHeadingAlias = "pageHeading";
        public const string PriorityTilesAlias = "priorityTiles";

        public PartyIdeas(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>(PartyIdeas.PageHeadingAlias);
            }
        }

        /// <summary>
        /// marked as internal as not needed in the view
        /// </summary>
        internal IEnumerable<PartyIdeaTile> PriorityTiles
        {
            get
            {
                return this.GetPropertyValue<Picker>(PartyIdeas.PriorityTilesAlias)
                            .AsPublishedContent()
                            .Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));
            }
        }

        /// <summary>
        /// gets a collection of all renderModel idea tiles in a specific rendering order
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PartyIdeaTile> GetPartyIdeaTiles()
        {
            List<PartyIdeaTile> partyIdeaTiles = new List<PartyIdeaTile>();

            // add the priority tiles to the top of the list
            partyIdeaTiles.AddRange(this.PriorityTiles);

            // add all remaining tiles (that are not in the priority list)
            partyIdeaTiles.AddRange(this.Descendants()
                                        .Select(x => PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x))
                                        .Where(x => x is PartyIdeaTile && !this.PriorityTiles.Select(y => y.Id).Contains(x.Id))
                                        .Cast<PartyIdeaTile>());

            return partyIdeaTiles;
        }
    }
}
