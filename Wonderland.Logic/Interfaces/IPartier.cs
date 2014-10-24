
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
        int Id { get; } // used for comparrison checks

        string FirstName { get; set;  }

        string LastName { get; set; }

        ProfileImage ProfileImage { get; set; }

        string ProfileImageUrl { get; } // Helper, could be replaced with ProfileImage.Url

        string PartyUrl { get; } // the custom url associated with the host or guest (host and guest method differ)

        Guid PartyGuid { get; }

        Address BillingAddress { get; set; }

        string Email { get; } // common property in BaseMemberType

        Guid ForgottenPasswordGuid { get; set; }

        int DotMailerId { get; set; }

        bool DotMailerRegistrationComplete { get; set; }

        bool FacebookRegistration { get; set; }
    }
}
