
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

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
    }
}
