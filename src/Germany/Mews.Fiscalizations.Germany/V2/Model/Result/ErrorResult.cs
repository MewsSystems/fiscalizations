using Mews.Fiscalizations.Core.Model;
using Newtonsoft.Json;

namespace Mews.Fiscalizations.Germany.V2.Model;

public sealed class ErrorResult
{
    private ErrorResult(string message, FiskalyError error, string content = null)
    {
        Message = message;
        Error = error;
        Content = content.AsNonEmpty();
    }

    public string Message { get; }

    public FiskalyError Error { get; }

    public Option<NonEmptyString> Content { get; }

    internal static ErrorResult Map(Dto.FiskalyErrorResponse error)
    {
        return new ErrorResult(
            message: error.Message,
            error: MapError(error)
        );
    }

    internal static ErrorResult MapException(JsonReaderException exception, string responseContent)
    {
        return new ErrorResult(
            message: exception.Message,
            error: FiskalyError.InvalidResponse,
            content: responseContent
        );
    }

    private static FiskalyError MapError(Dto.FiskalyErrorResponse error)
    {
        if (error is null)
        {
            return FiskalyError.InvalidResponse;
        }

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
            "E_TX_ILLEGAL_TYPE_CHANGE", _ => throw new InvalidOperationException($"Invalid request: {ToDebugString(error)}."),
            "E_TX_NO_TYPE_DEFINED", _ => throw new InvalidOperationException($"Invalid request: {ToDebugString(error)}."),
            "E_API_VERSION", _ => throw new InvalidOperationException($"Invalid request: {ToDebugString(error)}."),
            "E_CLIENT_NOT_FOUND", _ => FiskalyError.InvalidClientId,
            "E_TSS_NOT_FOUND", _ => FiskalyError.InvalidTssId,
            "E_TSS_CONFLICT", _ => FiskalyError.TssCreationConflict,
            "E_CLIENT_CONFLICT", _ => FiskalyError.ClientCreationConflict,
            "E_ACCESS_DENIED", _ => FiskalyError.TssAccessDenied,
            "E_EXPORT_IN_PROGRESS", _ => FiskalyError.ExportInProgress,
            "E_UNAUTHORIZED" , _ => FiskalyError.InvalidCredentials,
            _ => throw new NotImplementedException($"Unhandled fiskaly error: {ToDebugString(error)}.")
        );
    }

    private static string ToDebugString(Dto.FiskalyErrorResponse errorResponse)
    {
        return JsonConvert.SerializeObject(new
        {
            errorResponse.StatusCode,
            errorResponse.Code,
            errorResponse.Error,
            errorResponse.Message
        });
    }
}