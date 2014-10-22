
namespace Wonderland.Logic.Models.Media
{
    using System;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    internal static class ProfileImages
    {
        private const int Id = 5583;

        internal static int CreateProfileImage(HttpPostedFileBase profileImageFile)
        {
            IMediaService mediaService = ApplicationContext.Current.Services.MediaService;

            IMedia profileImage = mediaService.CreateMedia(Guid.NewGuid().ToString(), ProfileImages.Id, ProfileImage.Alias);

            profileImage.SetValue("umbracoFile", profileImageFile);

            mediaService.Save(profileImage);

            return profileImage.Id;
        }
    }
}
