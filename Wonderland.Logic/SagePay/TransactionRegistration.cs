
namespace Wonderland.Logic.SagePay
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;

    /// <summary>
    /// static helper used to send data to sage pay to register a new transaction
    /// </summary>
    internal static class TransactionRegistration
    {        
        internal static TransactionRegistrationResponse Send(TransactionRegistrationRequest transactionRegistrationRequest)
        {
            // serialize data into a url encoded querystring format for posting to sage pay
            string postData = SagePaySerializer.Serialize(transactionRegistrationRequest);
            byte[] postDataBytes = new UTF8Encoding().GetBytes(postData);

            Uri uri = new Uri("https://test.sagepay.com/simulator/VSPServerGateway.asp?Service=VendorRegisterTx");
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Method = "POST";
            webRequest.Timeout = (300 * 1000);
            webRequest.ContentType = "application/x-www-form-urlencoded";
            webRequest.ContentLength = postDataBytes.Length;

            using (var stream = webRequest.GetRequestStream())
            {
                stream.Write(postDataBytes, 0, postDataBytes.Length);
            }

            // post to SagePay and get response
            WebResponse webResponse = webRequest.GetResponse();

            string result;

            using (var reader = new StreamReader(webResponse.GetResponseStream(), Encoding.ASCII))
            {
                result = reader.ReadToEnd();
            }

            return SagePaySerializer.Deserialize<TransactionRegistrationResponse>(result);
        }
    }
}
