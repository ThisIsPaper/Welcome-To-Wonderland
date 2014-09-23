

namespace Wonderland.Logic.SagePay
{
    using System.ComponentModel;
    using Wonderland.Logic.Models.Entities;

    /// <summary>
    /// See appendix A in docs
    /// this will be serialized by a custom serializer for 'posting' to SagePay
    /// this class represents the data sent to sage pay to register a transaction
    /// </summary>
    public class TransactionRegistrationRequest
    {
        public TransactionRegistrationRequest(int vendorTxCode, decimal amount, bool allowGiftAid, string firstName, string lastName, Address address)
        {
            this.VendorTxCode = vendorTxCode;
            this.Amount = amount;
            this.AllowGiftAid = allowGiftAid;

            this.BillingSurname = lastName;
            this.BillingFirstnames = firstName;

            this.BillingAddress1 = address.Address1;
            this.BillingAddress2 = address.Address2;
            this.BillingCity = address.TownCity;
            this.BillingCountry = "UK";
            this.BillingPostCode = address.Postcode;

            this.DeliverySurname = lastName;
            this.DeliverFirstnames = firstName;

            this.DeliveryAddress1 = address.Address1;
            this.DeliveryAddress2 = address.Address2;
            this.DeliveryCity = address.TownCity;
            this.DeliveryCountry = "UK";
            this.DeliveryPostCode = address.Postcode;
        }

        public string VPSProtocol
        {
            get
            {
                return "3.00";
            }
        }

        public string TxType
        {
            get
            {
                return "PAYMENT";
            }
        }

        public string Vendor
        {
            get
            {
                return "macmillancance3";
            }
        }

        public int VendorTxCode
        {
            get;
            private set;
        }

        public decimal Amount
        {
            get;
            private set;
        }

        public string Currency
        {
            get
            {
                return "GBP";
            }
        }

        public string Description
        {
            get
            {
                return "Wonderland Donation";
            }
        }

        //[Unencoded]
        public string NotificationUrl
        {
            get
            {
                return "http://TODO_GET_FROM_CONFIG";
            }
        }

        // Token

        public string BillingSurname
        {
            get;
            private set;
        }

        public string BillingFirstnames
        {
            get;
            private set;
        }

        public string BillingAddress1
        {
            get;
            private set;
        }

        public string BillingAddress2
        {
            get;
            private set;
        }

        public string BillingCity
        {
            get;
            private set;
        }

        public string BillingPostCode
        {
            get;
            private set;
        }

        public string BillingCountry
        {
            get;
            private set;
        }

        // BillingState

        // BillingPhone

        public string DeliverySurname
        {
            get;
            private set;
        }

        public string DeliverFirstnames
        {
            get;
            private set;
        }

        public string DeliveryAddress1
        {
            get;
            private set;
        }

        public string DeliveryAddress2
        {
            get;
            private set;
        }

        public string DeliveryCity
        {
            get;
            private set;
        }

        public string DeliveryPostCode
        {
            get;
            private set;
        }

        public string DeliveryCountry
        {
            get;
            private set;
        }

        // Delivery State

        // Delivery Phone

        // Customer Email

        // Basket

        public bool AllowGiftAid
        {
            get;
            private set;
        }

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
