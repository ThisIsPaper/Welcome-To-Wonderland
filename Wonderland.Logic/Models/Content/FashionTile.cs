
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class FashionTile : PartyIdeaTile
    {
        public new const string Alias = "FashionTile";

        public FashionTile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
