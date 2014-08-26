
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class FashionCategory : PartyIdeaCategory
    {
        public new const string Alias = "FashionCategory";

        public FashionCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
