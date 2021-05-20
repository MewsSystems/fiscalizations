﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace Mews.Fiscalization.Germany.Dto
{
    internal partial class TransactionRequest
    {
        [JsonProperty("state")]
        [JsonConverter(typeof(StringEnumConverter))]
        public State State { get; set; }

        [JsonProperty("client_id")]
        public Guid ClientId { get; set; }
    }
}
