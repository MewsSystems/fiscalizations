using Mews.Fiscalizations.Fiskaly.DTOs;
using Mews.Fiscalizations.Fiskaly.Models;

namespace Mews.Fiscalizations.Fiskaly.Mappers;

internal static class ErrorMapper
{
    public static ErrorResult MapErrorResponse(this FiskalyErrorResponse errorResponse)
    {
        return new ErrorResult(
            Status: errorResponse.StatusCode,
            Code: errorResponse.Code,
            Error: errorResponse.Error,
            Message: errorResponse.Message
        );
    }
}