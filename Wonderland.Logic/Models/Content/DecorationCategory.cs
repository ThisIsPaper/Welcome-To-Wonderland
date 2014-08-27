
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class DecorationCategory : PartyIdeaCategory
    {
        public new const string Alias = "DecorationCategory";

        public DecorationCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
