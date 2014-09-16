

namespace Wonderland.Logic.Models.Entities
{
    using System;
    using Wonderland.Logic.Enums;
    using Wonderland.Logic.Models.Database;
    using Newtonsoft.Json;

    public class PartyWallItem
    {
        internal PartyWallItem(Donation donation)
        {
            this.PartyWallItemType = PartyWallItemType.Donation;
            this.Name = "donation debug";
            this.Timestamp = donation.Timestamp;
        }

        internal PartyWallItem(Message message)
        {
            this.PartyWallItemType = PartyWallItemType.Message;
            this.Name = "message debug";
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
