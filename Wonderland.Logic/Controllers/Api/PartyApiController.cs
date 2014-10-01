
namespace Wonderland.Logic.Controllers.Api
{
    using System;
    using System.Collections.Generic;
    using System.Web.Http;
    using Umbraco.Web.WebApi;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Extensions;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Entities;
    using Wonderland.Logic.Models.Forms;
    using Wonderland.Logic.Models.Members;
    

    public class PartyApiController : UmbracoApiController
    {
        [HttpGet]
        public IEnumerable<PartyWallItem> GetPartyWallItems([FromUri] Guid partyGuid, [FromUri] int take)
        {
            return this.GetPartyWallItems(partyGuid, DateTime.Now, take);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="partyGuid"></param>
        /// <param name="beforeDateTime"></param>
        /// <param name="take"></param>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<PartyWallItem> GetPartyWallItems([FromUri] Guid partyGuid, [FromUri] DateTime beforeDateTime, [FromUri] int take)
        {
            List<PartyWallItem> partyWallItems = new List<PartyWallItem>();

            string sql;

            if (this.Members.IsLoggedInPartier() && ((IPartier)this.Members.GetCurrentMember()).PartyGuid == partyGuid)
            {
                // get all donation and messages
                sql = @"
                        SELECT      TOP " + take.ToString() + @" *
                        FROM
                        (                            
                                SELECT      PartyWallItemType = " + (int)PartyWallItemType.Donation + @", 
                                            Id = NULL,
                                            MemberId, 
                                            Amount, 
                                            [Text] = NULL, 
                                            [Image] = NULL, 
                                            [Timestamp] 
                                FROM        wonderlandDonation
                                WHERE       Success = 1
                                            AND MemberId IN (SELECT MemberId FROM wonderlandMemberParty WHERE PartyGuid = @0)
                                            AND [Timestamp] < @1
                                UNION ALL
                                SELECT      PartyWallItemType = " + (int)PartyWallItemType.Message + @", 
                                            MessageId AS Id,
                                            MemberId, 
                                            Amount = NULL, 
                                            [Text], 
                                            [Image], 
                                            [Timestamp] 
                                FROM        wonderlandMessage
                                WHERE       MemberId IN (SELECT MemberId FROM wonderlandMemberParty WHERE PartyGuid = @0)
                                            AND [Timestamp] < @1
                        )
                        AS          PartyWallItems
                        ORDER BY    [Timestamp] DESC
                    ";
            }
            else
            {
                // only get the donations
                sql = @"
                        SELECT      TOP " + take.ToString() + @"
                                    PartyWallItemType = " + (int)PartyWallItemType.Donation + @", 
                                    ID = NULL,
                                    MemberId, 
                                    Amount, 
                                    [Text] = NULL, 
                                    [Image] = NULL, 
                                    [Timestamp] 
                        FROM        wonderlandDonation
                        WHERE       Success = 1
                                    AND MemberId IN (SELECT MemberId FROM wonderlandMemberParty WHERE PartyGuid = @0)
                                    AND [Timestamp] < @1
                        ORDER BY    [Timestamp] DESC
                    ";
            }
            
            // get a collection of all raw data, for all wall items for a party - as sorted by sql
            foreach (Donation_Message donation_Message in this.DatabaseContext.Database.Fetch<Donation_Message>(sql, partyGuid, beforeDateTime.ToUniversalTime()))
            {
                switch (donation_Message.PartyWallItemType)
                {
                    case PartyWallItemType.Donation:

                        partyWallItems.Add(new PartyWallItem((DonationRow)donation_Message));
                        break;

                    case PartyWallItemType.Message:

                        partyWallItems.Add(new PartyWallItem((MessageRow)donation_Message));                        
                        break;
                }
            }

            return partyWallItems;
        }

        [HttpPost]
        [MemberAuthorize(AllowType=PartyHost.Alias)]
        public FormResponse DeletePartyWallItem([FromBody] int messageId)
        {
            FormResponse formResponse = new FormResponse();

            PartyHost partyHost = (PartyHost)this.Members.GetCurrentMember();

            // is party host the host of the party associated with message being deleted ?
            MessageRow messageRow = this.DatabaseContext.Database.SingleOrDefault<MessageRow>(@"
                                                                                                SELECT  A.MessageId,
                                                                                                        A.MemberId,
                                                                                                        A.Text,
                                                                                                        A.Image,
                                                                                                        A.Timestamp 
                                                                                                FROM    wonderlandMessage A
                                                                                                        LEFT OUTER JOIN wonderlandMemberParty B ON A.MemberID = B.MemberId
                                                                                                WHERE   A.MessageId=@0 
                                                                                                        AND B.PartyGuid=@1",
                                                                                               messageId, 
                                                                                               partyHost.PartyGuid);
            
            if (messageRow != null)
            {
                // TODO: consider marking as deleted instead, and adapting all queries to account for this
                this.DatabaseContext.Database.Delete(messageRow);

                formResponse.Success = true;
            }

            return formResponse;
        }
    }
}
