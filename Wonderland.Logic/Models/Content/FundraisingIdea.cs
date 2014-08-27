
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class FundraisingIdea : SitePage
    {
        public new const string Alias = "FundraisingIdea";

        public FundraisingIdea(IPublishedContent content)
            : base(content)
        {
        }
    }
}
