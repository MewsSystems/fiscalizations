namespace Mews.Fiscalizations.Italy.Uniwix.Errors
{
    public sealed class ErrorResult
    {
        private ErrorResult(string message, ErrorType type)
        {
            Message = message;
            Type = type;
        }

        public string Message { get; }

        public ErrorType Type { get; }

        public static ErrorResult Create(string message, ErrorType type) => new ErrorResult(message, type);
    }
}
