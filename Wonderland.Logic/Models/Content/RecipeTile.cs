
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class RecipeIdeaTile : PartyIdeaTile
    {
        public new const string Alias = "RecipeIdeaTile";

        public RecipeIdeaTile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
