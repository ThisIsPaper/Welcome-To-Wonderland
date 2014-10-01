﻿
namespace Wonderland.Logic.Models.Forms
{
    using Newtonsoft.Json;
    using System.Collections.Generic;

    public class FormResponseFieldErrors
    {
        [JsonProperty("field")]
        public string Field { get; internal set; }

        [JsonProperty("errorMessages")]
        public IEnumerable<string> ErrorMessages { get; internal set; }
    }
}
