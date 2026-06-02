using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.Auth;

internal sealed class AuthorizationTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; init; }

    [JsonPropertyName("access_token_claims")]
    public AccessTokenClaims AccessTokenClaims { get; init; }

    [JsonPropertyName("access_token_expires_at")]
    public long AccessTokenExpiresAt { get; init; }
}

internal sealed class AccessTokenClaims
{
    [JsonPropertyName("env")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FiskalyEnvironment Environment { get; init; }
}
