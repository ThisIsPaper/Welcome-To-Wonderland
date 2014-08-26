
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class DecorationCategory : PartyIdeaCategory
    {
        public new const string Alias = "DecorationCategory";

        public DecorationCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
