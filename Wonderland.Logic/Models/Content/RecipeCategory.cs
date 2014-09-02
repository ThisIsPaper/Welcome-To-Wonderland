
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class RecipeCategory : PartyIdeaCategory
    {
        // Document Type
        public new const string Alias = "RecipeCategory";

        public RecipeCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
