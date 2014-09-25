
namespace Wonderland.Logic.SagePay
{
    using System.ComponentModel;

    // data sent from wonderland as a reply to the inbound notification request
    public class NotificationResponse
    {
        public NotificationStatus Status { get; set; }

        public string RedirectURL { get; set; }

        public string StatusDetail { get; set; }
    }
}
