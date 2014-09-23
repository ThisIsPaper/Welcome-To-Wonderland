
namespace Wonderland.Logic.Controllers.Api
{
    using Newtonsoft.Json.Linq;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.SagePay;

    public class SagePayApiController : UmbracoApiController
    {
        /// <summary>
        /// see: Step 9: Sage Pay contacts your NotificationURL
        /// ~/Umbraco/Api/SagePayApi/Notifcation/
        /// </summary>
        /// <param name="data"></param>
        [HttpPost]
        public object Notifcation([FromBody] JObject data)
        {
            
            // using post data, deserialize inot notification request;
            // based on request data, create a new notification response
            // return notification response



            //This POST contains a Status field that holds either:
            //OK          if the transaction was authorised.
            //PENDING     (for European Payment Types only), if the transaction has yet to be accepted or rejected
            //NOTAUTHED   if the authorisation was failed by the bank. 
            //ABORT       if the user decided to cancel the transaction whilst on our payment pages.
            //REJECTED    if your fraud screening rules were not met.
            //ERROR       if an error has occurred at Sage Pay. These are very infrequent, but your site should handle them anyway. They normally indicate a problem with bank connectivity. 

            //The StatusDetail field of the POST contains further human readable details about the Status field, explaining why a certain status was returned.




            // TODO: update wonderlandDonation


            //Your notification script should reply to the Sage Pay Server POST with three fields: Status, which 
            //indicates if you wish to accept the transaction notification, StatusDetail to hold human readable 
            //reasons for accepting the transaction or otherwise, and RedirectURL which is the completion page 
            //on your own site to which the customer should be redirected by the Sage Pay Server.

            // set the redirection URL - could either be:
            //  guest registered - success
            //  guest registered - fail
            //  party page - success
            //  party page - fail

            return null;
        }
    }
}
