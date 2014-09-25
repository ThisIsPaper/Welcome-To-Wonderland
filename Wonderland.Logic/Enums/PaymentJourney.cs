
namespace Wonderland.Logic.Enums
{
    /// <summary>
    /// used to set a field in the wonderlandDonation table, this is then used to determine the url to supply to Sage Pay for the final redirection
    /// </summary>
    public enum PaymentJourney
    {
        /// <summary>
        /// payment being made during the register guest signup
        /// </summary>
        RegisterGuest,

        /// <summary>
        /// payment being made on the main donate page
        /// </summary>
        Donate
    }
}
