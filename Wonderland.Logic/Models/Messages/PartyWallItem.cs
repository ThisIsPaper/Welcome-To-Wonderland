
namespace Wonderland.Logic.Models.Messages
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Database;
    using Wonderland.Logic.Models.Members;

    public class PartyWallItem
    {
        internal PartyWallItem(DonationRow donation)
        {
            IPartier partier = (IPartier)new MembershipHelper(UmbracoContext.Current).GetById((int)donation.MemberId);

            this.Id = -1;
            this.PartyWallItemType = PartyWallItemType.Donation;
            this.ThumbnailUrl = partier.ProfileImageUrl;
            this.IsPartyHost = partier is PartyHost && donation.PartyGuid == partier.PartyGuid;
            this.Name = partier.FirstName + " " + partier.LastName;
            this.Text = donation.Amount.ToString();
            this.Timestamp = donation.Timestamp.ToUniversalTime();
        }

        internal PartyWallItem(MessageRow message)
        {
            IPartier partier = (IPartier)new MembershipHelper(UmbracoContext.Current).GetById(message.MemberId);

            this.Id = message.MessageId;
            this.PartyWallItemType = PartyWallItemType.Message; 
            this.ThumbnailUrl = partier.ProfileImageUrl;
            this.IsPartyHost = partier is PartyHost;
            this.Name = partier.FirstName + " " + partier.LastName;
            this.Text = message.Text;
            
            if (message.Image != null)
            {
                this.ImageUrl = message.Image;
            }
            
            this.Timestamp = message.Timestamp.ToUniversalTime();
        }

        [JsonProperty("id")]
        public int Id
        {
            get;
            private set;
        }

        [JsonProperty("partyWallItemType")]
        public PartyWallItemType PartyWallItemType
        {
            get;
            private set;
        }

        [JsonProperty("thumbnailUrl")]
        public string ThumbnailUrl
        {
            get;
            private set;
        }

        [JsonProperty("isPartyHost")]
        public bool IsPartyHost
        {
            get;
            private set;
        }

        [JsonProperty("name")]
        public string Name
        {
            get;
            private set;
        }

        [JsonProperty("text")]
        public string Text
        {
            get;
            private set;
        }

        [DefaultValue(null)]
        [JsonProperty("imageUrl")]
        public string ImageUrl
        {
            get;
            private set;
        }

        [JsonProperty("timestamp")]
        public DateTime Timestamp
        {
            get;
            private set;
        }
    }
}
