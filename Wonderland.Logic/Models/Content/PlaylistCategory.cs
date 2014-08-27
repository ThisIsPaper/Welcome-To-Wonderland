
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class PlaylistCategory : PartyIdeaCategory
    {
        public new const string Alias = "PlaylistCategory";

        public PlaylistCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
