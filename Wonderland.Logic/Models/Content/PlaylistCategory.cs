
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class PlaylistCategory : PartyIdeaCategory
    {
        // Document Type
        public new const string Alias = "PlaylistCategory";

        public PlaylistCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
