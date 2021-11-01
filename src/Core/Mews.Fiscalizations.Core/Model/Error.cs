namespace Mews.Fiscalizations.Core.Model
{
    public class Error
    {
        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }
}