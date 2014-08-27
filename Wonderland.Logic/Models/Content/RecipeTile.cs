
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RecipeTile : PartyIdeaTile
    {
        public new const string Alias = "RecipeTile";

        public RecipeTile(IPublishedContent content)
            : base(content)
        {
        }

        public override string TileType
        {
            get
            {
                return "recipe";
            }
        }

        public string RecipeName
        {
            get
            {
                return this.GetPropertyValue<string>("recipeName");
            }
        }

        public IEnumerable<string> Ingredients
        {
            get
            {
                return this.GetPropertyValue<string[]>("ingredients");
            }
        }
    }
}
