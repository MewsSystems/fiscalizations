namespace Mews.Fiscalizations.Core.Model
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public static INonEmptyEnumerable<Error> Create(string message)
        {
            return new Error(message).ToEnumerable();
        }
    }
}