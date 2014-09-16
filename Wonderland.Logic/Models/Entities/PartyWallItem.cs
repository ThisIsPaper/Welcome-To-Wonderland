

namespace Wonderland.Logic.Models.Entities
{
    using System;
    using Wonderland.Logic.Enums;
    using Newtonsoft.Json;

    /// <summary>
    /// this class is serialized for the frontend rendering
    /// </summary>
    public class PartyWallItem
    {
        public PartyWallItem(PartyWallItemType partyWallItemType)
        {
            this.PartyWallItemType = partyWallItemType;
        }

        public PartyWallItemType PartyWallItemType { get; private set; }  // will determine how text is rendered (donation is bigger font)

        //public int Id { get; internal set; }

        public string ThumbnailUrl { get; internal set; } // usually a party guest or party host

        public string Name { get; internal set; }

        public string Text { get; internal set; }

        public string ImageUrl { get; internal set; }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; internal set; }
    }
}
