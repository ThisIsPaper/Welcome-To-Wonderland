
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;
    using Wonderland.Logic.Enums;

    [TableName("wonderlandDonation")]
    [ExplicitColumns]
    [PrimaryKey("VendorTxCode", autoIncrement=false)]
    public class DonationRow
    {
        public DonationRow()
        {
            this.VendorTxCode = Guid.NewGuid();
            this.Timestamp = DateTime.UtcNow;
        }

        [Column("VendorTxCode")]
        public Guid VendorTxCode { get; set; }

        [Column("PartyGuid")]
        public Guid PartyGuid { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("GiftAid")]
        public bool GiftAid { get; set; }

        [Column("MemberId")]
        public int? MemberId { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Address1")]
        public string Address1 { get; set; }

        [Column("Address2")]
        public string Address2 { get; set; }

        [Column("TownCity")]
        public string TownCity { get; set; }

        [Column("Postcode")]
        public string Postcode { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }

        [Column("PaymentJourney")]
        public PaymentJourney PaymentJourney { get; set; }

        [Column("VPSTxId")]
        public string VPSTxId { get; set; }

        [Column("SecurityKey")]
        public string SecurityKey { get; set; }

        [Column("Success")]
        public bool Success { get; set; }

        /// <summary>
        /// when true, indicates that the user timed-out on Sage Pay, or cancelled
        /// </summary>
        [Column("Cancelled")]
        public bool Cancelled { get; set; }

        public static explicit operator DonationRow(Donation_Message donation_Message)
        {
            return new DonationRow()
                        {
                            MemberId = donation_Message.MemberId,
                            Amount = donation_Message.Amount,
                            Timestamp = donation_Message.Timestamp
                        };
        }
    }
}

