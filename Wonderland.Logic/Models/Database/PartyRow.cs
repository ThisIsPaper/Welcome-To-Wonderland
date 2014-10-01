
namespace Wonderland.Logic.Models.Database
{
    using System;
    using Umbraco.Core.Persistence;

    [TableName("wonderlandParty")]
    [ExplicitColumns]
    public class PartyRow
    {
        public PartyRow(Guid partyGuid, string partyUrlIdentifier)
        {
            this.PartyGuid = partyGuid;
            this.PartyUrlIdentifier = partyUrlIdentifier;
        }

        [Column("PartyGuid")]
        public Guid PartyGuid { get; set; }

        [Column("PartyUrlIdentifier")]
        public string PartyUrlIdentifier { get; set; }
    }
}
