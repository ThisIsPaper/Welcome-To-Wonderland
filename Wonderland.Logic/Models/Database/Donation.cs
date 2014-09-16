
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandDonation")]
    [ExplicitColumns]
    public class Donation
    {
        [Column("MemberId")]
        internal int MemberId { get; set; }

        [Column("Amount")]
        internal decimal Amount { get; set; }

        [Column("Timestamp")]
        internal DateTime Timestamp { get; set; }
    }
}
