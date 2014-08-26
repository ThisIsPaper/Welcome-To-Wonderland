
namespace Wonderland.Logic.Models.Media
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;
    using Umbraco.Core.Models.PublishedContent;

    public class Image : PublishedContentModel
    {
        public const string Alias = "Image";

        public Image(IPublishedContent content)
            : base(content)
        {
        }

        public string ImageUrl
        {
            get
            {
                try
                {
                   return this.GetCropUrl(null);
                }
                catch
                {
                    return string.Empty; // TODO: fallback to a default image (set in CMS)
                }
            }
        }

        public string HtmlAltText
        {
            get
            {
                return this.GetPropertyValue<string>("htmlAltText");
            }
        }

    }
}
