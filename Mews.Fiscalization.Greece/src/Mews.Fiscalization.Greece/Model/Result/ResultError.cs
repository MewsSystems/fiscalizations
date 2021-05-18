using Mews.Fiscalization.Greece.Model.Types;
using System;

namespace Mews.Fiscalization.Greece.Model.Result
{
    public class ResultError
    {
        public ResultError(string code, string message)
        {
            Message = message;
            Code = MapErrorCode(code);
        }

        public ErrorCode Code { get; }

        public string Message { get; }

        private ErrorCode MapErrorCode(string code)
        {
            switch (code)
            {
                case SendInvoiceErrorCodes.InternalServerErrorCode:
                case "TechnicalError":
                    return ErrorCode.TechnicalError;
                case SendInvoiceErrorCodes.TimeoutErrorCode:
                    return ErrorCode.NetworkError;
                case SendInvoiceErrorCodes.UnauthorizedErrorCode:
                    return ErrorCode.InvalidCredentials;
                case "ValidationError":
                case "XmlSyntaxError":
                    return ErrorCode.ValidationError;
                default:
                    throw new NotImplementedException($"Error code: {code} is not implemented.");
            }
        }
    }
}
