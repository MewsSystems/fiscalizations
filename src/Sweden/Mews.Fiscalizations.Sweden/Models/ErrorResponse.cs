using FuncSharp;

namespace Mews.Fiscalizations.Sweden.Models;

public sealed class ErrorResponse(NonEmptyString error, Srv4posErrorType errorType, string message = null)
{
    public NonEmptyString Error { get; } = error;

    public Srv4posErrorType ErrorType { get; } = errorType;

    public Option<NonEmptyString> Message { get; } = message.AsNonEmpty();
}