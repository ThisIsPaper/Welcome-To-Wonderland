
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class FashionCategory : PartyIdeaCategory
    {
        public new const string Alias = "FashionCategory";

        public FashionCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
