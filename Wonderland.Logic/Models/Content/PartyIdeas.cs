
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class PartyIdeas : SitePage
    {
        public const string Alias = "PartyIdeas";

        public PartyIdeas(IPublishedContent content)
            : base(content)
        {
        }
    }
}
