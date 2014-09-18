

namespace Wonderland.Logic.Models.Entities
{
    using Newtonsoft.Json;
    using System;
    using System.ComponentModel;
    using Umbraco.Web;
    using Umbraco.Web.Security;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Interfaces;
    using Wonderland.Logic.Models.Database;

    public class PartyWallItem
    {
        internal PartyWallItem(Donation donation)
        {
            IPartier partier = (IPartier)new MembershipHelper(UmbracoContext.Current).GetById(donation.MemberId);   
            
            this.PartyWallItemType = PartyWallItemType.Donation;
            this.ThumbnailUrl = partier.ProfileImageUrl;
            this.Name = partier.PersonName;
            this.Text = donation.Amount.ToString();
            this.Timestamp = donation.Timestamp;
        }

        internal PartyWallItem(Message message)
        {
            IPartier partier = (IPartier)new MembershipHelper(UmbracoContext.Current).GetById(message.MemberId);   

            this.PartyWallItemType = PartyWallItemType.Message; 
            this.ThumbnailUrl = partier.ProfileImageUrl;
            this.Name = partier.PersonName;
            this.Text = message.Text;
            
            if (message.Image != null)
            {
                this.ImageUrl = "/Uploads/PartyWall/" + message.Image;
            }
            
            this.Timestamp = message.Timestamp;
        }

        [JsonIgnore]
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
