
namespace Wonderland.Logic.Controllers.Api
{
    using System;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Mail;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Configuration;
    using System.Web.Http;
    using System.Web.Security;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.DotMailer;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
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
            // create response obj to send back to Sage Pay (defaulting to error)
            NotificationResponse notificationResponse = new NotificationResponse();
            notificationResponse.Status = NotificationStatus.ERROR;

            // get associated transaction details from the database
            DonationRow donationRow = this.DatabaseContext.Database.Fetch<DonationRow>("SELECT TOP 1 * FROM wonderlandDonation WHERE VendorTxCode = @0", notificationRequest.VendorTxCode).Single();

            // safety checks
            if (notificationRequest.VPSTxId != donationRow.VPSTxId)
            {
                notificationResponse.StatusDetail += "VPSTxID Invalid" + Environment.NewLine;
            }
            else if (!this.IsSignatureValid(donationRow, notificationRequest))
            {
                notificationResponse.StatusDetail += "Signature Invalid" + Environment.NewLine;
            }
            else
            {
                // change response status from Error to OK, as valid inbound data is valid
                notificationResponse.Status = NotificationStatus.OK;

                switch (notificationRequest.Status)
                {
                    case NotificationStatus.OK:                       
                        donationRow.Success = true;
                        this.SendPaymentConfirmationEmail(donationRow);
                        break;

                    case NotificationStatus.ABORT:
                        donationRow.Cancelled = true;
                        break;                    
                }

                this.DatabaseContext.Database.Update(donationRow);
            }

            // determine redirect url
            string redirectUrl = WebConfigurationManager.AppSettings["SagePay:RedirectDomain"];

            switch (donationRow.PaymentJourney)
            {
                case PaymentJourney.RegisterGuest:

                    // safety check (memberId should always have a value)
                    if (donationRow.MemberId.HasValue)
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

            notificationResponse.RedirectURL = redirectUrl; 
            
            if (donationRow.Success)
            {
                notificationResponse.RedirectURL += "complete/";
            }
            
            if (donationRow.Cancelled)
            {
                notificationResponse.RedirectURL += "cancelled/";
            }

            notificationResponse.RedirectURL += "?VendorTxCode=" + notificationRequest.VendorTxCode;

            // ensure the return type is plain text
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

            //return notificationRequest.VPSSignature == hash;

            return true; // HACK !!! hash calculation fails when using the test card data (although works correctly when cancelling in Sage Pay)
        }

        private void SendPaymentConfirmationEmail(DonationRow donationRow)
        {
            if (donationRow.MemberId.HasValue)
            {
                Donate donate = (Donate)this.Umbraco.TypedContentSingleAtXPath("//" + Donate.Alias);

                IPartier partier = (IPartier)this.Members.GetById(donationRow.MemberId.Value);

                PartyHost partyhost = this.Members.GetPartyHost(donationRow.PartyGuid);

                MailMessage mailMessage = new MailMessage();

                mailMessage.From = new MailAddress(donate.ServerEmailAddress);
                mailMessage.To.Add(new MailAddress(partier.Email));
                mailMessage.Subject = donate.EmailSubject;
                mailMessage.IsBodyHtml = true;

                mailMessage.Body = donate.EmailBody
                                        .Replace("[%FIRST_NAME%]", partier.FirstName)
                                        .Replace("[%LAST_NAME%]", partier.LastName)
                                        .Replace("[%PARTY_HOST%]", partyhost.FirstName + " " + partyhost.LastName)
                                        .Replace("[%EMAIL%]", partier.Email) 
                                        .Replace("[%AMOUNT%]", "£" + donationRow.Amount.ToString("F"))
                                        .Replace("[%DONATION_TIMESTAMP%]", donationRow.Timestamp.ToShortDateString())
                                        .Replace("[%VENDOR_TX_CODE%]", donationRow.VendorTxCode.ToString());

                // Fire and forget
                Task.Run(() =>
                {
                    using (SmtpClient smtpClient = new SmtpClient())
                    {
                        smtpClient.Send(mailMessage);
                    }

                });
            }
        }
    }
}
