
namespace Wonderland.Logic.Models.Members
{
    /// <summary>
    /// common methods on both the PartyHost and PartyGuest
    /// </summary>
    public interface IPartier
    {
        /// <summary>
        /// rendered name
        /// </summary>
        string PersonName { get; }

        /// <summary>
        /// gets the custom party url associated with the the host or guest
        /// </summary>
        /// <returns></returns>
        string PartyUrl { get; }


        int Id { get; }
    }
}
