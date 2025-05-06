using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.Models;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES;

internal static class SignESErrorMapper
{
    public static ErrorResult MapSignESErrorResponse(this SignESErrorResponse errorResponse)
    {
        return new ErrorResult(
            Status: errorResponse.StatusCode,
            Code: errorResponse.Code,
            Error: errorResponse.Error,
            Message: errorResponse.Message
        );
    }
}