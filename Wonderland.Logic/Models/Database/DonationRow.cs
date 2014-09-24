
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandDonation")]
    [ExplicitColumns]
    [PrimaryKey("VendorTxCode", autoIncrement=true)]
    public class DonationRow
    {
        [Column("VendorTxCode")]
        public int VendorTxCode { get; set; }

        [Column("PartyGuid")]
        public Guid PartyGuid { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("GiftAid")]
        public bool GiftAid { get; set; }

        [Column("MemberId")]
        public int MemberId { get; set; }

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

        [Column("VPSTxId")]
        public string VPSTxId { get; set; }

        [Column("SecurityKey")]
        public string SecurityKey { get; set; }

        [Column("Success")]
        public bool Success { get; set; }

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

