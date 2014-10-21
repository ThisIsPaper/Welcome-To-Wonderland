
namespace Wonderland.Logic.Models.Media
{
    using System;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    internal static class PartyWallImages
    {
        private const int Id = 5582;

        internal static int CreatePartyWallImage(HttpPostedFileBase wallImageFile)
        {
            IMediaService mediaService = ApplicationContext.Current.Services.MediaService;

            IMedia wallImage = mediaService.CreateMedia(Guid.NewGuid().ToString(), PartyWallImages.Id, PartyWallImage.Alias);

            wallImage.SetValue("umbracoFile", wallImageFile);

            mediaService.Save(wallImage);

            return wallImage.Id;
        }
    }
}
