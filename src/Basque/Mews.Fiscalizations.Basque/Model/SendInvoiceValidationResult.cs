namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class SendInvoiceValidationResult
    {
        public SendInvoiceValidationResult(string errorCode, string description, string explanation)
        {
            ErrorCode = errorCode;
            Description = description;
            Explanation = explanation;
        }

        public string ErrorCode { get; } // TODO: enum?

        public string Description { get; }

        public string Explanation { get; }
    }
}