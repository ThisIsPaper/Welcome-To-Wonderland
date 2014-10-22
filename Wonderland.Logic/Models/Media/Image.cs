
namespace Wonderland.Logic.Models.Media
{
    using Newtonsoft.Json;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;
    using System.Web.Script.Serialization;

    public class Image : NonSerializablePublishedContentModel // PublishedContentModel
    {
        // Media Type
        public const string Alias = "Image";

        // Properties
        public const string AltTextAlias = "altText";

        public Image(IPublishedContent content)
            : base(content)
        {
        }
        
        [JsonProperty("altText")]
        public string AltText
        {
            get
            {
                return this.GetPropertyValue<string>(Image.AltTextAlias);
            }
        }
    }
}
