
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class FundraisingIdea : SitePage
    {
        public new const string Alias = "FundraisingIdea";

        public FundraisingIdea(IPublishedContent content)
            : base(content)
        {
        }
    }
}
