
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class FashionIdeaTile : PartyIdeaTile
    {
        public const string Alias = "FashionIdeaTile";

        public FashionIdeaTile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
