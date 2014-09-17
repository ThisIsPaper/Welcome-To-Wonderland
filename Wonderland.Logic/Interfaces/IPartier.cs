
namespace Wonderland.Logic.Interfaces
{
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

        /// <summary>
        /// rendered name
        /// </summary>
        string PersonName { get; }

        /// <summary>
        /// url to their profile image
        /// </summary>
        string ProfileImageUrl { get; }

        /// <summary>
        /// gets the custom url associated with the host or guest
        /// </summary>
        /// <returns></returns>
        string PartyUrl { get; }
    }
}
