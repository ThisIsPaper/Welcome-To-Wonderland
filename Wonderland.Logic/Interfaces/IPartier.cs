
namespace Wonderland.Logic.Interfaces
{
    using System;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Media;
    using Wonderland.Logic.Models.Members;

    /// <summary>
    /// common methods on both the PartyHost and PartyGuest
    /// this allows for (IPartier)this.Members.GetCurrentMember();
    /// </summary>
    public interface IPartier
    {
        /// <summary>
        /// used for comparrison checks
        /// </summary>
        int Id { get; }

        string FirstName { get; set;  }

        string LastName { get; set; }

        ProfileImage ProfileImage { get; set; }

        /// <summary>
        /// url to their profile image
        /// </summary>
        string ProfileImageUrl { get; }

        /// <summary>
        /// gets the custom url associated with the host or guest
        /// </summary>
        /// <returns></returns>
        string PartyUrl { get; }

        Guid PartyGuid { get; }

        Address BillingAddress { get; set; }

        /// <summary>
        /// common property in BaseMemberType
        /// </summary>
        string Email { get; }

        Guid ForgottenPasswordGuid { get; set; }

        int DotMailerId { get; set; }

        bool DotMailerRegistrationComplete { get; set; }

        bool FacebookRegistration { get; set; }
    }
}
