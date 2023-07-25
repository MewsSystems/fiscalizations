using FuncSharp;
using System;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class ErrorResult<TCode>
    where TCode : struct
{
    internal ErrorResult(TCode errorCode, string message = null)
    {
        Message = message;
        ErrorCode = errorCode;
    }

    public string Message { get; }

    public TCode ErrorCode { get; }

    internal static ErrorResult<ResultErrorCode> Map(Dto.GeneralErrorResponse response)
    {
        return new ErrorResult<ResultErrorCode>(
            message: response.result.message,
            errorCode: MapErrorCode(response.result.errorCode)
        );
    }

    internal static ResultErrorCode MapErrorCode(string errorCode)
    {
        return errorCode.Match(
            "INVALID_SECURITY_USER", _ => ResultErrorCode.InvalidCredentials,
            "NOT_REGISTERED_CUSTOMER", _ => ResultErrorCode.InvalidCredentials,
            "INVALID_CUSTOMER", _ => ResultErrorCode.InvalidCredentials,
            "INVALID_USER_RELATION", _ => ResultErrorCode.InvalidCredentials,
            "MAINTENANCE_MODE", _ => ResultErrorCode.MaintenanceMode,
            "FORBIDDEN", _ => ResultErrorCode.UnauthorizedUser,
            "INVALID_REQUEST_SIGNATURE", _ => ResultErrorCode.InvalidSigningKey,
            "INVALID_REQUEST", _ => ResultErrorCode.InvalidRequest,
            _ => throw new NotImplementedException($"Error code: {errorCode} is not implemented.")
        );
    }
}