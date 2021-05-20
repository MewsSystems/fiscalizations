﻿using Newtonsoft.Json;

namespace Mews.Fiscalization.Germany.Dto
{
    internal sealed class AuthorizationTokenRequest
    {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("api_secret")]
        public string ApiSecret { get; set; }
    }
}
