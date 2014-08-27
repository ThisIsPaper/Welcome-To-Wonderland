
namespace Wonderland.Logic.Models.Media
{
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class Image : PublishedContentModel
    {
        public const string Alias = "Image";

        public Image(IPublishedContent content)
            : base(content)
        {
        }

        public string AltText
        {
            get
            {
                return this.GetPropertyValue<string>("altText");
            }
        }
    }
}
