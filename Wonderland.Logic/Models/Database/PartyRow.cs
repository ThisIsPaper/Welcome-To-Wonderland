
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandParty")]
    [ExplicitColumns]
    public class PartyRow
    {
        public PartyRow(Guid partyGuid)
        {
            this.PartyGuid = partyGuid;
        }

        [Column("PartyGuid")]
        public Guid PartyGuid { get; set; }
    }
}
