
namespace Wonderland.Logic.Models.Media
{
    using Newtonsoft.Json;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Interfaces;
    using AST.S3.Helper;

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

        public override string Url
        {
            get
            {
                //UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

                //return MediaHelper.ParseMedia(this.Id.ToString(), umbracoHelper).Url;

                return base.Url;
            }
        }
    }
}
