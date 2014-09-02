
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class RecipeTile : PartyIdeaTile
    {
        // Document Type
        public new const string Alias = "RecipeTile";

        // Properties
        public const string RecipeNameAlias = "recipeName";
        public const string IngredientsAlias = "ingredients";

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
                return this.GetPropertyValue<string>(RecipeTile.RecipeNameAlias);
            }
        }

        public IEnumerable<string> Ingredients
        {
            get
            {
                return this.GetPropertyValue<string[]>(RecipeTile.IngredientsAlias);
            }
        }
    }
}
