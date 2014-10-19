
namespace Wonderland.Logic.Models.Messages
{
    using Newtonsoft.Json;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class FormResponse
    {
        [JsonProperty("success")]
        [DefaultValue(false)]
        public bool Success { get; internal set; }

        [JsonProperty("message")]
        [DefaultValue("")]
        public string Message { get; internal set; }

        [JsonProperty("errors")]
        public IEnumerable<FormResponseFieldErrors> Errors { get; internal set; }
    }
}
