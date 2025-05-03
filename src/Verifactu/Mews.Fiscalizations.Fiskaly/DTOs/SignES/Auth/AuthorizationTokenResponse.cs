using System.Text.Json.Serialization;

namespace Mews.Fiscalizations.Fiskaly.DTOs.SignES.Auth;

internal sealed class AuthorizationTokenResponse
{
    [JsonPropertyName("content")]
    public AuthorizationTokenDataResponse DataResponse { get; init; }
}

internal sealed class AuthorizationTokenDataResponse
{
    [JsonPropertyName("access_token")]
    public AccessTokenResponse AccessTokenResponse { get; init; }

    [JsonPropertyName("refresh_token")]
    public AccessTokenResponse RefreshTokenResponse { get; init; }

    [JsonPropertyName("claims")]
    public Claims Claims { get; init; }
}

internal sealed class AccessTokenResponse
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

