
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
        //public const string PartyGuidAlias = "partyGuid";
        //public const string FirstNameAlias = "firstName";
        //public const string LastNameAlias = "lastName";
        //public const string BillingAddressAlias = "billingAddress";
        public const string ProfileImageAlias = "profileImage";
        //public const string ForgottenPasswordGuidAlias = "forgottenPasswordGuid";
        //public const string DotMailerIdAlias = "dotMailerId";
        //public const string DotMailerRegistrationCompleteAlias = "dotMailerRegistrationComplete";
        //public const string FacebookRegistrationAlias = "facebookRegistration";

        public Partier(IPublishedContent content)
            : base(content)
        {
        }

        public ProfileImage ProfileImage
        {
            get
            {
                int? imageId = (int?)this.GetPropertyValue(Partier.ProfileImageAlias);

                if (imageId.HasValue && imageId > 0)
                {
                    return (ProfileImage)this.Umbraco.TypedMedia(imageId);
                }

                return null;
            }
            set
            {
                if (value != null)
                {
                    this.SetPropertyValue(Partier.ProfileImageAlias, value.Id);
                }
                else
                {
                    // TODO: remove any existing profile image

                    this.SetPropertyValue(Partier.ProfileImageAlias, null);
                }
            }
        }
    }
}
