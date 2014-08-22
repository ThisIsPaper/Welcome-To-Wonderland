
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class DecorationTile : PartyIdeaTile
    {
        public new const string Alias = "DecorationTile";

        public DecorationTile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
