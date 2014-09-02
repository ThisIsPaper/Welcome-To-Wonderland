
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class FashionCategory : PartyIdeaCategory
    {
        // Document Type
        public new const string Alias = "FashionCategory";

        public FashionCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
