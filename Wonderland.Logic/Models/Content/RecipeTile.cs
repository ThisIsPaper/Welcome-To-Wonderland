
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

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
