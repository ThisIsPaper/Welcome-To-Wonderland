
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class FundraisingCategory : PartyIdeaCategory
    {
        // Document Type
        public new const string Alias = "FundraisingCategory";

        public FundraisingCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
