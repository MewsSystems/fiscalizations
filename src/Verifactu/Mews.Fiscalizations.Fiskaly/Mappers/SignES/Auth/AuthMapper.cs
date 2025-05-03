using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Auth;
using Mews.Fiscalizations.Fiskaly.Models;
using FiskalyEnvironment = Mews.Fiscalizations.Fiskaly.DTOs.FiskalyEnvironment;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Auth;

internal static class AuthMapper
{
    public static AccessToken MapAuthResponse(this AuthorizationTokenResponse response)
    {
        return new AccessToken(
            value: response.DataResponse.AccessTokenResponse.Bearer,
            environment: response.DataResponse.Claims.Environment == FiskalyEnvironment.TEST ? Models.FiskalyEnvironment.Test : Models.FiskalyEnvironment.Production,
            expirationUtc: response.DataResponse.AccessTokenResponse.ExpiresAt.FromUnixTime()
        );
    }
}