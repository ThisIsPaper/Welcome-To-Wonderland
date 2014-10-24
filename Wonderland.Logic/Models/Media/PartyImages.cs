
namespace Wonderland.Logic.Models.Media
{
    using System;
    using System.Web;
    using Umbraco.Core;
    using Umbraco.Core.Models;
    using Umbraco.Core.Services;
    using Umbraco.Web;

    internal static class PartyImages
    {
        private const int Id = 5581;

        internal static PartyImage CreatePartyImage(HttpPostedFileBase customPartyImageFile)
        {
            IMediaService mediaService = ApplicationContext.Current.Services.MediaService;

            UmbracoHelper umbracoHelper = new UmbracoHelper(UmbracoContext.Current);

            IMedia partyImage = mediaService.CreateMedia(Guid.NewGuid().ToString(), PartyImages.Id, PartyImage.Alias);
            
            // TODO: swap to the PartyImage obj to set this
            partyImage.SetValue("umbracoFile", customPartyImageFile);

            mediaService.Save(partyImage);

            return (PartyImage)umbracoHelper.TypedMedia(partyImage.Id);           
        }
    }
}
