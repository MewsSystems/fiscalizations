using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Verifactu.DTOs;

internal sealed class AuthorizationTokenResponse
{
    [JsonPropertyName("content")]
    public AuthorizationTokenData Data { get; init; }
}

internal sealed class AuthorizationTokenData
{
    [JsonPropertyName("access_token")]
    public AccessToken AccessToken { get; init; }

    [JsonPropertyName("refresh_token")]
    public AccessToken RefreshToken { get; init; }

    [JsonPropertyName("claims")]
    public Claims Claims { get; init; }
}

internal sealed class AccessToken
{
    [JsonPropertyName("bearer")]
    public string Bearer { get; init; }

    [JsonPropertyName("expires_at")]
    public long ExpiresAt { get; init; }

    [JsonPropertyName("expires_in")]
    public long ExpiresIn { get; init; }
}

internal sealed class Claims
{
    [JsonPropertyName("environment")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public FiskalyEnvironment Environment { get; init; }

    [JsonPropertyName("organization_id")]
    public Guid OrganizationId { get; init; }
}

