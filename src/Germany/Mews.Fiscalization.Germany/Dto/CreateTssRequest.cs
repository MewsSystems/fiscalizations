﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Mews.Fiscalization.Germany.Dto
{
    public sealed class CreateTssRequest
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public TssState State { get; set; }
    }
}
