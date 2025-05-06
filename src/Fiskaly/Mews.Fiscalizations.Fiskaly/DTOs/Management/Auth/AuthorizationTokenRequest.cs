using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.Management.Auth;


internal sealed class AuthorizationTokenRequest
{
    [JsonPropertyName("api_key")]
    public string ApiKey { get; set; }

    [JsonPropertyName("api_secret")]
    public string ApiSecret { get; set; }
}