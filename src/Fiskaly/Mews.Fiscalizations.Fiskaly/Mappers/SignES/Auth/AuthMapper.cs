using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Auth;
using Mews.Fiscalizations.Fiskaly.Models;
using FiskalyEnvironment = Mews.Fiscalizations.Fiskaly.DTOs.FiskalyEnvironment;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Auth;

internal static class AuthMapper
{
    public static AccessToken MapAuthResponse(this ContentWrapper<AuthorizationTokenResponse> response)
    {
        return new AccessToken(
            value: response.Content.AccessTokenResponse.Bearer,
            environment: response.Content.Claims.Environment == FiskalyEnvironment.TEST ? Models.FiskalyEnvironment.Test : Models.FiskalyEnvironment.Production,
            expirationUtc: DateTimeOffset.FromUnixTimeSeconds(response.Content.AccessTokenResponse.ExpiresAt).DateTime
        );
    }
}