
namespace Wonderland.Logic.Controllers.Api
{
    using Newtonsoft.Json.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.SagePay;
    

    public class SagePayApiController : UmbracoApiController
    {
        /// <summary>
        /// see: Step 9: Sage Pay contacts your NotificationURL
        /// ~/Umbraco/Api/SagePayApi/Notifcation/
        /// </summary>
        /// <param name="notificationRequest"></param>
        [HttpPost]
        public HttpResponseMessage Notifcation([FromBody] NotificationRequest notificationRequest)
        {
            NotificationResponse notificationResponse = new NotificationResponse();

            // find transaction in db, and update status
            var t = notificationRequest.VendorTxCode;

            if (notificationRequest.Status == NotificationStatus.OK)
            {
                notificationResponse.Status = NotificationStatus.OK;
                notificationResponse.RedirectURL = "http://wonderland.local/";

            }            
            else
            {

            }


            //Your notification script should reply to the Sage Pay Server POST with three fields: Status, which 
            //indicates if you wish to accept the transaction notification, StatusDetail to hold human readable 
            //reasons for accepting the transaction or otherwise, and RedirectURL which is the completion page 
            //on your own site to which the customer should be redirected by the Sage Pay Server.

            // set the redirection URL - could either be:
            //  guest registered - success
            //  guest registered - fail
            //  party page - success
            //  party page - fail

            // HACK: to ensure the return type is plain text
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(
                                    SagePaySerializer.SerializeResponse(notificationResponse),
                                    Encoding.UTF8,
                                    "text/plain")
            };
        }
    }
}
