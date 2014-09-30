
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandMemberParty")]
    [PrimaryKey("MemberId", autoIncrement=false)]
    [ExplicitColumns]
    public class MemberPartyRow
    {
        internal MemberPartyRow(int memberId, Guid partyGuid)
        {
            this.MemberId = memberId;
            this.PartyGuid = partyGuid;
            this.Timestamp = DateTime.Now;
        }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("PartyGuid")]
        public Guid PartyGuid { get; set; }

        [Column("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}
