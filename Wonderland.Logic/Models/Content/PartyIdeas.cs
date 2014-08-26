﻿
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

        /// <summary>
        /// marked as internal as not needed in the view model
        /// </summary>
        internal IEnumerable<PartyIdeaTile> PriorityTiles
        {
            get
            {
                return this.GetPropertyValue<Picker>("priorityTiles")
                            .AsPublishedContent()
                            .Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));
            }
        }

        /// <summary>
        /// gets a collection of all party idea tiles in a specific rendering order
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

            

            //// reuse the built-in IPublishedContent -> Model factory
            //return this.Descendants()
            //            .Where(x => 
            //                    x.DocumentTypeAlias == DecorationTile.Alias ||
            //                    x.DocumentTypeAlias == FashionTile.Alias ||
            //                    x.DocumentTypeAlias == PlaylistTile.Alias ||
            //                    x.DocumentTypeAlias == RecipeTile.Alias)
            //            .Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));


            return partyIdeaTiles;
        }
    }
}
