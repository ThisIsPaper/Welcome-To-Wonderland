
namespace Wonderland.Logic.Models.Members
{
    using System;
    using Umbraco.Core.Models;
    using Umbraco.Web;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Media;

    public abstract class Partier : BaseMemberType
    {
        // Properties
        private const string FirstNameAlias = "firstName";
        private const string LastNameAlias = "lastName";
        private const string ProfileImageAlias = "profileImage";
        private const string PartyGuidAlias = "partyGuid";
        private const string BillingAddressAlias = "billingAddress";
        private const string ForgottenPasswordGuidAlias = "forgottenPasswordGuid";
        private const string DotMailerIdAlias = "dotMailerId";
        private const string DotMailerRegistrationCompleteAlias = "dotMailerRegistrationComplete";
        private const string FacebookRegistrationAlias = "facebookRegistration";

        // Local Cache
        private string firstName = null;
        private string lastName = null;
        private ProfileImage profileImage = null;
        private Guid? partyGuid = null;
        private Address billingAddress = null;


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

        public Address BillingAddress
        {
            get
            {
                if (this.billingAddress != null)
                {
                    return this.billingAddress;
                }

                return new Address(this.GetPropertyValue<string>(PartyHost.BillingAddressAlias));
            }
            set
            {
                this.billingAddress = value;
                this.SetPropertyValue(PartyHost.BillingAddressAlias, value.ToString());
            }
        }

        public Guid ForgottenPasswordGuid
        {
            get
            {
                return this.GetPropertyValue<Guid>(PartyHost.ForgottenPasswordGuidAlias);
            }
            set
            {
                this.SetPropertyValue(PartyHost.ForgottenPasswordGuidAlias, value.ToString("D").ToLower());
            }
        }

        public int DotMailerId
        {
            get
            {
                return this.GetPropertyValue<int>(Partier.DotMailerIdAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.DotMailerIdAlias, value);
            }
        }

        public bool DotMailerRegistrationComplete
        {
            get
            {
                return this.GetPropertyValue<bool>(Partier.DotMailerRegistrationCompleteAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.DotMailerRegistrationCompleteAlias, value);
            }
        }

        public bool FacebookRegistration
        {
            get
            {
                return this.GetPropertyValue<bool>(Partier.FacebookRegistrationAlias);
            }
            set
            {
                this.SetPropertyValue(Partier.FacebookRegistrationAlias, value);
            }
        }
    }
}
