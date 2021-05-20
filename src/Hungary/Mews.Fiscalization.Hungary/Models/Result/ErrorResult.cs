using System;

namespace Mews.Fiscalization.Hungary.Models
{
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
            switch (errorCode)
            {
                case "INVALID_SECURITY_USER":
                case "NOT_REGISTERED_CUSTOMER":
                case "INVALID_CUSTOMER":
                case "INVALID_USER_RELATION":
                    return ResultErrorCode.InvalidCredentials;
                case "MAINTENANCE_MODE":
                    return ResultErrorCode.MaintenanceMode;
                case "FORBIDDEN":
                    return ResultErrorCode.UnauthorizedUser;
                case "INVALID_REQUEST_SIGNATURE":
                    return ResultErrorCode.InvalidSigningKey;
                default:
                    throw new NotImplementedException($"Error code: {errorCode} is not implemented.");
            }
        }
    }
}
