
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class Party : SitePage
    {
        public new const string Alias = "Party";

        public Party(IPublishedContent content)
            : base(content)
        {
        }
    }
}
