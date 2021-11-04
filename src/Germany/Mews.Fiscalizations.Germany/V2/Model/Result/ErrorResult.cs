using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Newtonsoft.Json;
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

            if (error.StatusCode.SucceedsOrEquals(500) && error.StatusCode.PreceedsOrEquals(599))
            {
                return FiskalyError.ServerSide;
            }

            return error.Code.Match(
                "E_TX_UPSERT", _ => FiskalyError.InvalidTransactionOperation,
                "E_TSS_DISABLED", _ => FiskalyError.InvalidTssOperation,
                "E_TSS_NOT_INITIALIZED", _ => FiskalyError.InvalidTssOperation,
                "E_TX_ILLEGAL_TYPE_CHANGE", _ => throw new InvalidOperationException($"Invalid request from the server: {ToDebugString(error)}."),
                "E_TX_NO_TYPE_DEFINED", _ => throw new InvalidOperationException($"Invalid request from the server: {ToDebugString(error)}."),
                "E_API_VERSION", _ => throw new InvalidOperationException($"Invalid request from the server: {ToDebugString(error)}."),
                "E_CLIENT_NOT_FOUND", _ => FiskalyError.InvalidClientId,
                "E_TSS_NOT_FOUND", _ => FiskalyError.InvalidTssId,
                "E_TSS_CONFLICT", _ => FiskalyError.TssCreationConflict,
                "E_CLIENT_CONFLICT", _ => FiskalyError.ClientCreationConflict,
                _ => throw new NotImplementedException($"Unhandled fiskaly error: {ToDebugString(error)}."),
            );
        }

        private static string ToDebugString(Dto.FiskalyErrorResponse errorResponse)
        {
            return JsonConvert.SerializeObject(new
            {
                StatusCode = errorResponse.StatusCode,
                Code = errorResponse.Code,
                Error = errorResponse.Error,
                Message = errorResponse.Message
            });
        }
    }
}
