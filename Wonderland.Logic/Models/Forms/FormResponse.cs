﻿
namespace Wonderland.Logic.Models.Forms
{
    using Newtonsoft.Json;
    using System.ComponentModel;

    public class FormResponse
    {
        [JsonProperty("success")]
        [DefaultValue(false)]
        public bool Success { get; internal set; }

        [JsonProperty("message")]
        [DefaultValue("")]
        public string Message { get; internal set; }
    }
}