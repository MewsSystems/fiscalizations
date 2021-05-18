﻿using Newtonsoft.Json;

namespace Mews.Fiscalization.Germany.Dto
{
    internal sealed class FiskalyErrorResponse
    {
        [JsonProperty("status_code")]
        public string StatusCode { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }

        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}
