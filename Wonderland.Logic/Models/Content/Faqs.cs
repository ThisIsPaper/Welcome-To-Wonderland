
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Faqs : SitePage
    {
        public new const string Alias = "Faqs";

        public Faqs(IPublishedContent content) : base(content)
        {
        }
    }
}
