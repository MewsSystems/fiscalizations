using FuncSharp;
using System;

namespace Mews.Fiscalizations.Germany.V2.Model
{
    public sealed class ErrorResult
    {
        private ErrorResult(string message, FiskalyError error)
        {
            Message = message;
            Error = error;
        }

        public string Message { get; }

        public FiskalyError Error { get; }

        internal static ErrorResult Map(Dto.FiskalyErrorResponse error)
        {
            return new ErrorResult(
                message: error.Message,
                error: MapError(error)
            );
        }

        internal static FiskalyError MapError(Dto.FiskalyErrorResponse error)
        {
            // For some reason, when the credentials are invalid, Fiskaly returns null code but with a message.
            if (error.Code == null && error.Message.Equals("Invalid credentials") || error.Error.Equals("Unauthorized"))
            {
                return FiskalyError.InvalidCredentials;
            }

            return error.Code.Match(
                "E_TX_UPSERT", _ => FiskalyError.InvalidTransactionOperation,
                "E_TSS_DISABLED", _ => FiskalyError.InvalidTssOperation,
                "E_TSS_NOT_INITIALIZED", _ => FiskalyError.InvalidTssOperation,
                "E_TX_ILLEGAL_TYPE_CHANGE", _ => throw new InvalidOperationException($"Invalid request from the library with error code: {error.Code}"),
                "E_TX_NO_TYPE_DEFINED", _ => throw new InvalidOperationException($"Invalid request from the library with error code: {error.Code}"),
                "E_API_VERSION", _ => throw new InvalidOperationException($"Invalid request from the library with error code: {error.Code}"),
                "E_CLIENT_NOT_FOUND", _ => FiskalyError.InvalidClientId,
                "E_TSS_NOT_FOUND", _ => FiskalyError.InvalidTssId,
                _ => throw new NotImplementedException($"Error code: {error.Code} is not implemented.")
            );
        }
    }
}
