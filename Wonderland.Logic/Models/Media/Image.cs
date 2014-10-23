
namespace Wonderland.Logic.Models.Media
{
    using Newtonsoft.Json;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Interfaces;

    public class Image : SerializablePublishedContentModel, IPartyImage
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
