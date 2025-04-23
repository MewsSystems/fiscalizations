using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class AuthorizationTokenRequest
{
    [JsonPropertyName("content")]
    public AuthorizationTokenRequestContent Content { get; init; }
}

internal sealed class AuthorizationTokenRequestContent
{
    [JsonPropertyName("api_key")]
    public string ApiKey { get; init; }

    [JsonPropertyName("api_secret")]
    public string ApiSecret { get; init; }
}