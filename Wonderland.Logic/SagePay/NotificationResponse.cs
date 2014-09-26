
namespace Wonderland.Logic.SagePay
{
    /// <summary>
    /// data sent from wonderland to Sage Pay as a reply to the inbound notification request 
    /// </summary>
    public class NotificationResponse
    {
        /// <summary>
        /// indicates if you wish to accept the transaction notification
        /// </summary>
        public NotificationStatus Status { get; set; }

        /// <summary>
        /// completion page on your own site to which the customer should be redirected by the Sage Pay Serve
        /// </summary>
        public string RedirectURL { get; set; }

        /// <summary>
        /// human readable reasons for accepting the transaction or otherwise
        /// </summary>
        public string StatusDetail { get; set; }
    }
}
