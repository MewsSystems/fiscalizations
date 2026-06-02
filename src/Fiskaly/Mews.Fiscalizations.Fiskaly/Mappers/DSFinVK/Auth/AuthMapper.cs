using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.Auth;
using Mews.Fiscalizations.Fiskaly.Models;
using FiskalyEnvironmentDto = Mews.Fiscalizations.Fiskaly.DTOs.FiskalyEnvironment;

namespace Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.Auth;

internal static class AuthMapper
{
    public static AccessToken MapAuthResponse(this AuthorizationTokenResponse response)
    {
        return new AccessToken(
            value: response.AccessToken,
            environment: response.AccessTokenClaims.Environment == FiskalyEnvironmentDto.TEST
                ? FiskalyEnvironment.Test
                : FiskalyEnvironment.Production,
            expirationUtc: DateTimeOffset.FromUnixTimeSeconds(response.AccessTokenExpiresAt).DateTime
        );
    }
}
