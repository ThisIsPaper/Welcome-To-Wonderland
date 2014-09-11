
namespace Wonderland.Logic.Models.Api
{

    using Newtonsoft.Json;
    using System.ComponentModel;

    /// <summary>
    /// common response from api controller methods 
    /// </summary>
    public class ApiResponse
    {
        [JsonProperty("success")]
        [DefaultValue(false)]
        public bool Success { get; internal set; }

        [JsonProperty("message")]
        [DefaultValue("")]
        public string Message { get; internal set; }
    }
}
