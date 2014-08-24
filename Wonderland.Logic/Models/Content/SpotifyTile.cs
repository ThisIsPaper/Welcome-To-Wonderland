﻿
namespace Wonderland.Logic.Models.Content
{
    using System.Collections.Generic;
    using System.Linq;
    using Umbraco.Web;
    using Umbraco.Core.Models;
    using nuPickers;

    public class SpotifyTile : PartyIdeaTile
    {
        public new const string Alias = "SpotifyTile";

        public SpotifyTile(IPublishedContent content)
            : base(content)
        {
        }

        public override string TileType
        {
            get
            {
                return "spotify";
            }
        }
    }
}
