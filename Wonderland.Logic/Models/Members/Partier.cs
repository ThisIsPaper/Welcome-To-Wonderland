
namespace Wonderland.Logic.Models.Members
{
    using System;
    using System.Linq;
    using System.Web.Security;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Media;

    public abstract class Partier : BaseMemberType
    {
        // Properties
        public const string ProfileImageAlias = "profileImage";

        // local cache
        private ProfileImage profileImage = null;

        public Partier(IPublishedContent content)
            : base(content)
        {
        }

        public ProfileImage ProfileImage
        {
            get
            {
                if (this.profileImage != null)
                {
                    return this.profileImage;
                }

                int? imageId = (int?)this.GetPropertyValue(Partier.ProfileImageAlias);

                if (imageId.HasValue && imageId > 0)
                {
                    return (ProfileImage)this.Umbraco.TypedMedia(imageId);
                }

                return null;
            }
            set
            {
                this.profileImage = value;

                if (value == null)
                {
                    // delete any associated profile image from media
                    if (this.ProfileImage != null)
                    {
                        IMedia media = this.MediaService.GetById(this.ProfileImage.Id);
                        this.MediaService.Delete(media);
                    }

                    // remove reference
                    this.SetPropertyValue(Partier.ProfileImageAlias, null);                    
                }
                else
                {
                    // update reference
                    this.SetPropertyValue(Partier.ProfileImageAlias, value.Id);
                }
            }
        }
    }
}
