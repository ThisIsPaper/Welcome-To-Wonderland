
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
        private const string FirstNameAlias = "firstName";
        private const string LastNameAlias = "lastName";
        private const string ProfileImageAlias = "profileImage";
        private const string PartyGuidAlias = "partyGuid";

        // local cache
        private string firstName = null;
        private string lastName = null;
        private ProfileImage profileImage = null;
        private Guid? partyGuid = null;


        public Partier(IPublishedContent content)
            : base(content)
        {
        }

        public string FirstName
        {
            get
            {
                if (this.firstName != null)
                {
                    return this.firstName;
                }

                return this.GetPropertyValue<string>(Partier.FirstNameAlias) ?? String.Empty;
            }
            set
            {
                this.firstName = value;
                this.SetPropertyValue(Partier.FirstNameAlias, value);
            }
        }

        public string LastName
        {
            get
            {
                if (this.lastName != null)
                {
                    return this.lastName;
                }

                return this.GetPropertyValue<string>(Partier.LastNameAlias) ?? String.Empty;
            }
            set
            {
                this.lastName = value;
                this.SetPropertyValue(Partier.LastNameAlias, value);
            }
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
        
        public string ProfileImageUrl
        {
            get
            {
                if (this.ProfileImage != null)
                {
                    return this.ProfileImage.Url; //TODO:S3URL
                }

                return null;
            }
        }

        /// <summary>
        /// once created, never changes - this guid identifies a specific party
        /// </summary>
        public Guid PartyGuid
        {
            get
            {
                if (this.partyGuid.HasValue)
                {
                    return this.partyGuid.Value;
                }

                return this.GetPropertyValue<Guid>(Partier.PartyGuidAlias);
            }
            set
            {
                this.partyGuid = value;
                this.SetPropertyValue(Partier.PartyGuidAlias, value.ToString());
            }
        }


    }
}
