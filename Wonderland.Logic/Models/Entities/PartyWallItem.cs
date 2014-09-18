

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
            //this.ImageUrl = null;
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
                this.ImageUrl = "/Uploads/Wall/TODO/";
            }
            
            this.Timestamp = message.Timestamp;
        }

        public PartyWallItemType PartyWallItemType
        {
            get;
            private set;
        }

        public string ThumbnailUrl
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public string Text
        {
            get;
            private set;
        }

        [DefaultValue(null)]
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
