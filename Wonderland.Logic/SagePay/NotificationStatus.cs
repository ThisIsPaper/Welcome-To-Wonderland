
namespace Wonderland.Logic.SagePay
{
    public enum NotificationStatus
    {
        /// <summary>
        /// Process executed without error
        /// </summary>
        OK,

        /// <summary>
        /// The Sage Pay gateway could not authorise the transaction because the details provided by the customer were incorrect, or insufficient funds were available. However the transaction has completed
        /// </summary>
        NOTAUTHED,

        /// <summary>
        /// This only affects European Payment methods. Indicates a transaction has yet to fail or succeed. This will be updated by Sage Pay when we recieve a notification from PPRO. The updated status can be seen in MySagePay
        /// </summary>
        PENDING,

        /// <summary>
        /// The Transaction could not be completed because the user clicked the CANCEL button on the payment pages, or went inactive for 15 minutes ot longer
        /// </summary>
        ABORT,

        /// <summary>
        /// The Sage Pay System rejected the transaction because of the fraud screening rules you have set on your account. Note: The bank may have authorised the transaction but your own rule bases for AVS/CV2 or 3D-Secuure caused the transaction to be rejected
        /// </summary>
        REJECTED,
    
        /// <summary>
        /// The 3D-Secure chekcs were performed successfully and the card details secured at Sage Pay. Only returned if TxType is AUTHENTICATE
        /// </summary>
        AUTHENTICATED,

        /// <summary>
        /// 3D-Secure checks failed or were not performed, but the card details are still secured at Sage Pay. Only returned if the TxType is AUTENTICATE
        /// </summary>
        REGISTERED,

        /// <summary>
        /// A problem occurred at Sage Pay which prevented transaction registration. Please notify Sage Pay with Vendor, VendorTxCode and StatusDetail
        /// </summary>
        ERROR
    }
}
