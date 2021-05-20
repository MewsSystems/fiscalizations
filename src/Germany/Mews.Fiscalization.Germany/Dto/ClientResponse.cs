﻿using Newtonsoft.Json;
using System;

namespace Mews.Fiscalization.Germany.Dto
{
    internal sealed class ClientResponse
    {
        [JsonProperty("serial_number")]
        public string SerialNumber { get; set; }

        [JsonProperty("time_creation")]
        public long TimeCreation { get; set; }

        [JsonProperty("time_update")]
        public long TimeUpdate { get; set; }

        [JsonProperty("tss_id")]
        public Guid TssId { get; set; }

        [JsonProperty("_id")]
        public Guid Id { get; set; }

        [JsonProperty("_type")]
        public string Type { get; set; }

        [JsonProperty("_env")]
        public string Env { get; set; }

        [JsonProperty("_version")]
        public string Version { get; set; }
    }
}
