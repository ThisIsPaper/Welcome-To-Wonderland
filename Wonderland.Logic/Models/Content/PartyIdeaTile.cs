
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class PartyIdeaTile : SitePage
    {
        public const string Alias = "PartyIdeaTile";

        public PartyIdeaTile(IPublishedContent content)
            : base(content)
        {
        }
    }
}
