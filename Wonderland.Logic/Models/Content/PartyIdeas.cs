
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Umbraco.Core.Models.PublishedContent;

    public class PartyIdeas : SitePage
    {
        public new const string Alias = "PartyIdeas";

        public PartyIdeas(IPublishedContent content)
            : base(content)
        {
        }

        public string PageHeading
        {
            get
            {
                return this.GetPropertyValue<string>("pageHeading");
            }
        }

        public IEnumerable<PartyIdeaTile> PartyIdeaTiles
        {
            get
            {
                // reuse the built-in IPublishedContent -> Model factory
                return this.Children().Select(x => (PartyIdeaTile)PublishedContentModelFactoryResolver.Current.Factory.CreateModel(x));
            }
        }
    }
}
