using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.Management.Auth;

internal sealed class AuthorizationTokenResponse
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("access_token_claims")]
    public AccessTokenClaims AccessTokenClaims { get; set; }

    [JsonPropertyName("access_token_expires_in")]
    public int AccessTokenExpiresIn { get; set; }

    [JsonPropertyName("access_token_expires_at")]
    public int AccessTokenExpiresAt { get; set; }

    [JsonPropertyName("refresh_token")]
    public string RefreshToken { get; set; }

    [JsonPropertyName("refresh_token_expires_in")]
    public int RefreshTokenExpiresIn { get; set; }
    
    [JsonPropertyName("refresh_token_expires_at")]
    public int RefreshTokenExpiresAt { get; set; }
}

internal sealed class AccessTokenClaims
{
    [JsonPropertyName("environment")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FiskalyEnvironment Environment { get; init; }

    [JsonPropertyName("organization_id")]
    public Guid OrganizationId { get; init; }
}

