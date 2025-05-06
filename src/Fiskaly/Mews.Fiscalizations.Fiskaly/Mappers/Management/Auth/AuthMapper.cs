using Mews.Fiscalizations.Fiskaly.DTOs.Management.Auth;
using Mews.Fiscalizations.Fiskaly.Models;
using FiskalyEnvironment = Mews.Fiscalizations.Fiskaly.DTOs.FiskalyEnvironment;

namespace Mews.Fiscalizations.Fiskaly.Mappers.Management.Auth;

internal static class AuthMapper
{
    public static AccessToken MapAuthResponse(this AuthorizationTokenResponse response)
    {
        return new AccessToken(
            value: response.AccessToken,
            environment: response.AccessTokenClaims.Environment == FiskalyEnvironment.TEST ? Models.FiskalyEnvironment.Test : Models.FiskalyEnvironment.Production,
            expirationUtc: DateTimeOffset.FromUnixTimeSeconds(response.AccessTokenExpiresAt).DateTime
        );
    }
}