
namespace Wonderland.Logic.SagePay
{
    /// <summary>
    /// Response types that could be received from SagePay
    /// </summary>
    public enum TransactionRegistrationStatus
    {
        /// <summary>
        /// Process executed without error
        /// </summary>
        OK,

        /// <summary>
        /// If the VendorTxCode passed in has been used before, but that transaction is still active, then details of that transaction are passed back
        /// </summary>
        OKREPEATED,

        /// <summary>
        /// Input message was missing fields or badly formatted - normally will only occur during development
        /// </summary>
        MALFORMED,

        /// <summary>
        /// Transaction was not registered becuase although the POST format was valid, some information supplied was invalid (eg. incorrect currency)
        /// </summary>
        INVALID,

        /// <summary>
        /// /A problem occured at Sage Pay which prevented transaction registration. Please notify SagePay with Vendor, VendorTxCode and StatusDetail
        /// </summary>
        ERROR
    }
}
