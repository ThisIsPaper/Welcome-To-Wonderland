
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;
    using Wonderland.Logic.Enums;

    /// <summary>
    /// used to return data from both the donation and message tables in a single sql hit, where all items are sorted by the timestamp
    /// </summary>
    [ExplicitColumns]
    public class Donation_Message
    {
        [ResultColumn("PartyWallItemType")]
        public PartyWallItemType PartyWallItemType { get; set; }

        [ResultColumn("MemberID")]
        public int MemberId { get; set; }

        [ResultColumn("Amount")]
        public decimal Amount { get; set; }

        [ResultColumn("Text")]
        public string Text { get; set; }

        [ResultColumn("Image")]
        public string Image { get; set; }

        [ResultColumn("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
