
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandMemberParty")]
    [PrimaryKey("MemberId", autoIncrement=false)]
    [ExplicitColumns]
    public class MemberParty
    {
        internal MemberParty(int memberId, Guid partyGuid)
        {
            this.MemberId = memberId;
            this.PartyGuid = partyGuid;
        }

        [Column("MemberId")]
        public int MemberId { get; set; }

        [Column("PartyGuid")]
        public Guid PartyGuid { get; set; }
    }
}
