
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class FundraisingTile : PartyIdeaTile
    {
        // Document Type
        public new const string Alias = "FundraisingTile";

        // Properties

        public FundraisingTile(IPublishedContent content)
            : base(content)
        {
        }

        public override string TileType
        {
            get
            {
                return "fundraising";
            }
        }
    }
}
