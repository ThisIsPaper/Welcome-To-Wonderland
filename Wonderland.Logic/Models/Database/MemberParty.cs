
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandMemberParty")]
    [PrimaryKey("MemberId")]
    [ExplicitColumns]
    internal class MemberParty
    {
        [Column("MemberId")]
        internal int MemberId { get; set; }

        [Column("PartyGuid")]
        internal Guid PartyGuid { get; set; }
    }
}
