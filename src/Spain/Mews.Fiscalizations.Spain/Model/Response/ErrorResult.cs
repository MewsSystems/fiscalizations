namespace Mews.Fiscalizations.Spain.Model.Response
{
    public sealed class ErrorResult
    {
        public ErrorResult(string code, string message)
        {
            Code = code;
            Message = message;
        }

        public string Code { get; }
        public string Message { get; }
    }
}
