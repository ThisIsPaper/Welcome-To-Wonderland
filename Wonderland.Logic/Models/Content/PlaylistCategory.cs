
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class PlaylistCategory : PartyIdeaCategory
    {
        public new const string Alias = "PlaylistCategory";

        public PlaylistCategory(IPublishedContent content)
            : base(content)
        {
        }
    }
}
