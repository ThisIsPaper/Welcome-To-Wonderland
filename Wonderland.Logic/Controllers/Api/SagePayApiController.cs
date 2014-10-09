
namespace Wonderland.Logic.Controllers.Api
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Text;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Security;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Models.Content;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Members;
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
            notificationResponse.Status = NotificationStatus.ERROR;

            // get transaction details from the database via primary key
            // DonationRow donationRow = this.DatabaseContext.Database.Single<DonationRow>(notificationRequest.VendorTxCode); // doens't work with enums
            DonationRow donationRow = this.DatabaseContext.Database.Fetch<DonationRow>("SELECT TOP 1 * FROM wonderlandDonation WHERE VendorTxCode = @0", notificationRequest.VendorTxCode).Single();

            // safety check
            if (notificationRequest.VPSTxId != donationRow.VPSTxId)
            {
                notificationResponse.StatusDetail += "VPSTxIDs didn't match" + Environment.NewLine;
            }

            if (notificationRequest.Status == NotificationStatus.OK)
            {
                //if (!this.IsSignatureValid(donationRow, notificationRequest))
                //{
                //    notificationResponse.StatusDetail += "Signature MD5 didn't match" + Environment.NewLine;
                //}
                //else
                //{
                    notificationResponse.Status = NotificationStatus.OK;

                    // set success flag in db
                    donationRow.Success = true;

                    this.DatabaseContext.Database.Update(donationRow);
                //}
            }

            // determine redirect url
            string redirectUrl = WebConfigurationManager.AppSettings["SagePay:RedirectDomain"];

            switch (donationRow.PaymentJourney)
            {
                case PaymentJourney.RegisterGuest:

                    if (donationRow.MemberId.HasValue) // safety check - memberId should alwayws be present
                    {
                        // update dot mailer to indicate guest has fully registered
                        DotMailerService.GuestRegistrationCompleted((Contact)(PartyGuest)this.Members.GetById(donationRow.MemberId.Value));
                    }

                    redirectUrl += this.Umbraco.TypedContentSingleAtXPath("//" + RegisterGuest.Alias).Url;
                    break;

                case PaymentJourney.Donate:

                    redirectUrl += this.Umbraco.TypedContentSingleAtXPath("//" + Donate.Alias).Url;
                    break;
            }

            //update dot mailer donation_amount and guest_count for associated party host
            DotMailerService.UpdateContact((Contact)this.Members.GetPartyHost(donationRow.PartyGuid));

            notificationResponse.RedirectURL = redirectUrl + "?VendorTxCode=" + notificationRequest.VendorTxCode;


            // HACK: to ensure the return type is plain text
            return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
            {
                Content = new StringContent(
                                    SagePaySerializer.SerializeResponse(notificationResponse),
                                    Encoding.UTF8,
                                    "text/plain")
            };
        }

        private bool IsSignatureValid(DonationRow donationRow, NotificationRequest notificationRequest)
        {
            StringBuilder stringBuilder = new StringBuilder();
            
            stringBuilder.Append(notificationRequest.VPSTxId);
            stringBuilder.Append(notificationRequest.VendorTxCode);
            stringBuilder.Append(notificationRequest.Status.ToString());
            stringBuilder.Append(notificationRequest.TxAuthNo);
            stringBuilder.Append(WebConfigurationManager.AppSettings["SagePay:Vendor"].ToLower());  // local value used in conjunction with sent data
            stringBuilder.Append(notificationRequest.AVSCV2);
            stringBuilder.Append(donationRow.SecurityKey);                                          // local value used in conjunction with sent data
            stringBuilder.Append(notificationRequest.AddressResult);
            stringBuilder.Append(notificationRequest.PostCodeResult);
            stringBuilder.Append(notificationRequest.CV2Result);
            stringBuilder.Append(Convert.ToInt32(notificationRequest.GiftAid));
            stringBuilder.Append(notificationRequest.ThreeDSecureStatus);
            stringBuilder.Append(notificationRequest.CAVV);
            stringBuilder.Append(notificationRequest.AddressStatus);
            stringBuilder.Append(notificationRequest.PayerStatus);
            stringBuilder.Append(notificationRequest.CardType);
            stringBuilder.Append(notificationRequest.Last4Digits);
            stringBuilder.Append(notificationRequest.DeclineCode);
            stringBuilder.Append(notificationRequest.ExpiryDate);
            stringBuilder.Append(notificationRequest.FraudResponse);
            stringBuilder.Append(notificationRequest.BankAuthCode);

            string hash = FormsAuthentication.HashPasswordForStoringInConfigFile(stringBuilder.ToString(), "MD5");

            return notificationRequest.VPSSignature == hash;
        }
    }
}
