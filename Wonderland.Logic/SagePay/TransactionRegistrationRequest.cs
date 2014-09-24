
namespace Wonderland.Logic.SagePay
{
    using System.ComponentModel;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Database;
    using Umbraco.Web;
    using Umbraco.Web.Mvc;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Interfaces;

    /// <summary>
    /// See appendix A in docs
    /// this will be serialized by a custom serializer for 'posting' to SagePay
    /// this class represents the data sent to sage pay to register a transaction
    /// </summary>
    public class TransactionRegistrationRequest
    {
        public TransactionRegistrationRequest(DonationRow donationRow)
        {
            this.VendorTxCode = donationRow.VendorTxCode;
            this.Amount = donationRow.Amount;
            this.AllowGiftAid = donationRow.GiftAid;

            this.BillingSurname = donationRow.LastName;
            this.BillingFirstnames = donationRow.FirstName;

            this.BillingAddress1 = donationRow.Address1;
            this.BillingAddress2 = donationRow.Address2;
            this.BillingCity = donationRow.TownCity;
            this.BillingCountry = "UK";
            this.BillingPostCode = donationRow.Postcode;

            this.DeliverySurname = donationRow.LastName;
            this.DeliveryFirstnames = donationRow.FirstName;

            this.DeliveryAddress1 = donationRow.Address1;
            this.DeliveryAddress2 = donationRow.Address2;
            this.DeliveryCity = donationRow.TownCity;
            this.DeliveryCountry = "UK";
            this.DeliveryPostCode = donationRow.Postcode;

            // if member known, then add custer email
            if (donationRow.MemberId.HasValue)
            {
                this.CustomerEmail = ((IPartier)new MembershipHelper(UmbracoContext.Current).GetById((int)donationRow.MemberId)).Email;
            }

        }

        public string VPSProtocol { get { return "2.23"; } } //3.00

        public string TxType { get { return "PAYMENT"; } }

        // using the simulator account
        public string Vendor { get { return "macmillancanceS"; } } // live / test = macmillancance3

        public int VendorTxCode { get; private set; }

        public decimal Amount { get; private set; }

        public string Currency { get { return "GBP"; } }

        public string Description { get { return "Wonderland Donation"; } }

        //[Unencoded]
        public string NotificationUrl
        {
            get
            {
                return "http://TODO_GET_FROM_CONFIG";
            }
        }

        // Token

        public string BillingSurname { get; private set; }

        public string BillingFirstnames { get; private set; }

        public string BillingAddress1 { get; private set; }

        public string BillingAddress2 { get; private set; }

        public string BillingCity { get; private set; }

        public string BillingPostCode { get; private set; }

        public string BillingCountry { get; private set; }

        // BillingState

        // BillingPhone

        public string DeliverySurname { get; private set; }

        public string DeliveryFirstnames { get; private set; }

        public string DeliveryAddress1 { get; private set; }

        public string DeliveryAddress2 { get; private set; }

        public string DeliveryCity { get; private set; }

        public string DeliveryPostCode { get; private set; }

        public string DeliveryCountry { get; private set; }

        // Delivery State

        // Delivery Phone

        public string CustomerEmail { get; private set; }

        // Basket

        public bool AllowGiftAid { get; private set; }

        // ApplyAVSCV2

        // Apply3DSecure

        // Profile

        // BillingAgreement

        // AccountType

        // CreateToken

        // StoreToken

        // BasketXML

        // CustomerXML

        // SurchargeXML

        // VendorData

        // ReferrerID

        // Language

        // Website

        // FIRecipientAcctNumber

        // FIRecipientSurname

        // FIRecipientPostcode

        // FIRecipientDoB
    }
}
