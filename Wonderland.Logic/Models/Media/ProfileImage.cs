
namespace Wonderland.Logic.Models.Media
{
    using System.Web;
    using Umbraco.Core.Models;
    using Umbraco.Core.Models.PublishedContent;
    using Umbraco.Web;

    public class ProfileImage : BaseMediaType
    {
        // Media Type
        public const string Alias = "ProfileImage";

        // Properties
        public const string ImageAlias = "umbracoFile";

        public ProfileImage(IPublishedContent content)
            : base(content)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpPostedFileBase"></param>
        /// <returns>the url to the newly uploaded image</returns>
        internal string UploadImage(HttpPostedFileBase httpPostedFileBase)
        {
            this.SetPropertyValue(ProfileImage.ImageAlias, httpPostedFileBase);

            return this.Media.GetValue(ProfileImage.ImageAlias).ToString();
        }
    }
}
