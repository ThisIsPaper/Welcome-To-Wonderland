
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Core.Models;
    using Umbraco.Web;

    public class FundraisingIdeas : SitePage
    {
        public new const string Alias = "FundraisingIdeas";

        public FundraisingIdeas(IPublishedContent content)
            : base(content)
        {
        }

        public IEnumerable<FundraisingIdea> GetFundraisingIdeas()
        {
            return this.Children().Select(x => new FundraisingIdea(x));
        }
    }
}
