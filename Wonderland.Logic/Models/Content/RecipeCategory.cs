
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class RecipeCategory : PartyIdeaCategory
    {
        public new const string Alias = "RecipeCategory";

        public RecipeCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
