
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class Image : PublishedContentModel
    {
        // Media Type
        public const string Alias = "Image";

        // Properties
        public const string AltTextAlias = "altText";

        public Image(IPublishedContent content)
            : base(content)
        {
        }

        public string AltText
        {
            get
            {
                return this.GetPropertyValue<string>(Image.AltTextAlias);
            }
        }
    }
}
