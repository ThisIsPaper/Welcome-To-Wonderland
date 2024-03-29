﻿
namespace Wonderland.Logic.Models.Messages
{
    using Newtonsoft.Json;

    /// <summary>
    /// supplied when registering or logging-in by Facebook 
    /// </summary>
    public class FacebookCredentials
    {
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; }

        [JsonProperty("userId")]
        public string UserId { get; set; }

        [JsonProperty("signedRequest")]
        public string SignedRequest { get; set; }
    }
}