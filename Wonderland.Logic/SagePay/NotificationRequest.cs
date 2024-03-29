﻿
using System;
namespace Wonderland.Logic.SagePay
{
    /// <summary>
    /// inbound http data from sage pay 
    /// </summary>
    public class NotificationRequest
    {
        public string VPSProtocol { get; set; }

        public string TxType { get; set; }

        public Guid VendorTxCode { get; set; } // NOTE: this may need to be a string

        public string VPSTxId { get; set; } // added after examining supplied post

        public NotificationStatus Status { get; set; }

        public string StatusDetail { get; set; }

        public string TxAuthNo { get; set; }

        public string AVSCV2 { get; set; }

        public string AddressResult { get; set; }

        public string PostCodeResult { get; set; }
        
        public string CV2Result { get; set; }

        public bool GiftAid { get; set; }

        public string ThreeDSecureStatus { get; set; }

        public string CAVV { get; set; }
        
        public string AddressStatus { get; set; }
        
        public string PayerStatus { get; set; }
        
        public string CardType { get; set; }
        
        public string Last4Digits { get; set; }

        public string VPSSignature { get; set; }

        public string FraudResponse { get; set; }

        public string Surcharge { get; set; }

        public string DeclineCode { get; set; }

        public string ExpiryDate { get; set; }

        public string BankAuthCode { get; set; }

        public string Token { get; set; }
    }
}
