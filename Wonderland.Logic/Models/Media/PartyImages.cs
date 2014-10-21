
namespace Wonderland.Logic.Models.Media
{
    using System;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;

    internal static class PartyImages
    {
        private const int Id = 5581;

        internal static int CreatePartyImage(HttpPostedFileBase customPartyImageFile)
        {
            IMediaService mediaService = ApplicationContext.Current.Services.MediaService;

            IMedia partyImage = mediaService.CreateMedia(Guid.NewGuid().ToString(), PartyImages.Id, PartyImage.Alias);
            
            partyImage.SetValue("umbracoFile", customPartyImageFile);

            mediaService.Save(partyImage);
            
            return partyImage.Id;
        }
    }
}
