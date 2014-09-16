
namespace Wonderland.Logic.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;

    public class PartyApiController : UmbracoApiController
    {
        [HttpGet]
        public IEnumerable<PartyWallItem> GetPartyWallItems([FromUri] Guid partyGuid, [FromUri] int skip, [FromUri] int take)
        {
            List<PartyWallItem> partyWallItems = new List<PartyWallItem>();

            string sql = @"
                            SELECT      PartyWallItemType = " + (int)PartyWallItemType.Donation + @", 
                                        MemberId, 
                                        Amount, 
                                        [Text] = NULL, 
                                        [Image] = NULL, 
                                        [Timestamp] 
                            FROM        wonderlandDonation
                            UNION ALL
                            SELECT      PartyWallItemType = " + (int)PartyWallItemType.Message + @", 
                                        MemberId, 
                                        Amount = NULL, 
                                        [Text], 
                                        [Image], 
                                        [Timestamp] 
                            FROM        wonderlandMessage
                            WHERE       MemberId IN (SELECT MemberId FROM wonderlandMemberParty WHERE PartyGuid = @0)
                            ORDER BY    [Timestamp] DESC
                        ";
            
            // get a collection of all raw data, for all wall items for a party - as sorted by sql
            foreach (Donation_Message donation_Message in this.DatabaseContext.Database.Fetch<Donation_Message>(sql, partyGuid))
            {
                switch (donation_Message.PartyWallItemType)
                {
                    case PartyWallItemType.Donation:

                        partyWallItems.Add(new PartyWallItem((Donation)donation_Message));
                        break;

                    case PartyWallItemType.Message:

                        partyWallItems.Add(new PartyWallItem((Message)donation_Message));                        
                        break;
                }
            }

            return partyWallItems;
        }
    }
}
