
namespace Wonderland.Logic.Models.Content
{
    using Umbraco.Core.Models;

    public class Faqs : SitePage
    {
        public new const string Alias = "Faqs";

        public Faqs(IPublishedContent content) : base(content)
        {
        }
    }
}
