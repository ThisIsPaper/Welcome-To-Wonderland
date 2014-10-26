
namespace Wonderland.Logic.Models.Media
{
    using AST.S3.Helper;
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

        public string S3Url
        {
            get
            {
                UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

                return MediaHelper.ParseMedia(this.Id.ToString(), umbracoHelper).Url;
            }
        }   

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpPostedFileBase"></param>
        /// <returns>the url to the newly uploaded image</returns>
        internal string UploadImage(HttpPostedFileBase httpPostedFileBase)
        {
            this.SetPropertyValue(PartyImage.ImageAlias, httpPostedFileBase);

            return this.Media.GetValue(PartyImage.ImageAlias).ToString();
        }
    }
}
