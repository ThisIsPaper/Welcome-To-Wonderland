
namespace Wonderland.Logic.Interfaces
{
    using System;
    using Wonderland.Logic.Models.Entities;
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

        string FirstName { get; }

        string LastName { get; }

        /// <summary>
        /// url to their profile image
        /// </summary>
        string ProfileImageUrl { get; }

        /// <summary>
        /// gets the custom url associated with the host or guest
        /// </summary>
        /// <returns></returns>
        string PartyUrl { get; }

        /// <summary>
        /// 
        /// </summary>
        Guid PartyGuid { get; }

        /// <summary>
        /// 
        /// </summary>
        Address BillingAddress { get; set; }

        /// <summary>
        /// common property in BaseMemberType
        /// </summary>
        string Email { get; }
    }
}
