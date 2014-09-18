
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandDonation")]
    [ExplicitColumns]
    public class Donation
    {
        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("Amount")]
        public decimal Amount { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }

        public static explicit operator Donation(Donation_Message donation_Message)
        {
            return new Donation()
                        {
                            MemberId = donation_Message.MemberId,
                            Amount = donation_Message.Amount,
                            Timestamp = donation_Message.Timestamp
                        };
        }
    }
}
