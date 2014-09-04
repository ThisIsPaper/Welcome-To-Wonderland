
namespace Wonderland.Logic.Models.Members
{
    /// <summary>
    /// common methods on both the PartyHost and PartyGuest
    /// this allows for (IPartier)this.Members.GetCurrentMember();
    /// </summary>
    public interface IPartier
    {
        /// <summary>
        /// rendered name
        /// </summary>
        string PersonName { get; }

        /// <summary>
        /// gets the custom renderModel url associated with the host or guest
        /// </summary>
        /// <returns></returns>
        string PartyUrl { get; }

        /// <summary>
        /// used for comparrison checks
        /// </summary>
        int Id { get; }
    }
}
