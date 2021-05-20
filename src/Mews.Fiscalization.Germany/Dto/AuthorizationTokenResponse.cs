﻿using Newtonsoft.Json;

namespace Mews.Fiscalization.Germany.Dto
{
    internal sealed class AuthorizationTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("access_token_expires_in")]
        public long AccessTokenExpiresIn { get; set; }

        [JsonProperty("access_token_expires_at")]
        public long AccessTokenExpiresAt { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("refresh_token_expires_in")]
        public long RefreshTokenExpiresIn { get; set; }

        [JsonProperty("refresh_token_expires_at")]
        public long RefreshTokenExpiresAt { get; set; }
    }
}
