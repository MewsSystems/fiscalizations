namespace Mews.Fiscalizations.Italy.Uniwix.Errors;

public sealed class ErrorResult
{
    private ErrorResult(string message, ErrorType type, IEnumerable<string> errors = null)
    {
        Message = message;
        Type = type;
        Errors = errors.ToOption();
    }

    public string Message { get; }

    public ErrorType Type { get; }

    public Option<IEnumerable<string>> Errors { get; }

    public static ErrorResult Create(string message, ErrorType type, IEnumerable<string> errors = null)
    {
        return new ErrorResult(message, type, errors);
    }
}