
namespace Wonderland.Logic.Models.Media
{
    using System.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using Wonderland.Logic.Interfaces;

    public class PartyImage : BaseMediaType, IPartyImage
    {
        // Media Type
        public const string Alias = "PartyImage";

        // Properties
        public const string ImageAlias = "umbracoFile";

        public PartyImage(IPublishedContent content)
            : base(content)
        {
        }

        public HttpPostedFileBase Image
        {
            set
            {
                this.SetPropertyValue(PartyImage.ImageAlias, value);
            }
        }

    }
}
