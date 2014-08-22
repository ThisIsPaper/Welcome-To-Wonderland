
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class Wonderlanders : SitePage
    {
        public const string Alias = "Wonderlanders";

        public Wonderlanders(IPublishedContent content)
            : base(content)
        {
        }
    }
}
