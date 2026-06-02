using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.Auth;

internal sealed class AuthorizationTokenRequest
{
    [JsonPropertyName("api_key")]
    public string ApiKey { get; init; }

    [JsonPropertyName("api_secret")]
    public string ApiSecret { get; init; }
}
