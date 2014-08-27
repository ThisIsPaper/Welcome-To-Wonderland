
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class RecipeCategory : PartyIdeaCategory
    {
        public new const string Alias = "RecipeCategory";

        public RecipeCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
