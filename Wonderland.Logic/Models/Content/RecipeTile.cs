
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
        public const string IngredientsHeadingAlias = "ingredientsHeading"; 
        public const string IngredientsAlias = "ingredients";
        public const string PreparationCopyAlias = "preparationCopy";

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

        public string IngredientsHeading
        {
            get
            {
                return this.GetPropertyValue<string>(RecipeTile.IngredientsHeadingAlias);
            }
        }

        public IEnumerable<string> Ingredients
        {
            get
            {
                return this.GetPropertyValue<string[]>(RecipeTile.IngredientsAlias).Where(x => !string.IsNullOrWhiteSpace(x));
            }
        }

        public string PreparationCopy
        {
            get
            {
                return this.GetPropertyValue<string>(RecipeTile.PreparationCopyAlias);
            }
        }

    }
}
