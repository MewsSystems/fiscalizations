namespace Mews.Fiscalizations.TicketBai.Model
{
    public sealed class ValidationResult
    {
        public ValidationResult(string errorCode, string description, string explanation)
        {
            ErrorCode = errorCode;
            Description = description;
            Explanation = explanation;
        }

        public string ErrorCode { get; } // TODO: enum

        public string Description { get; }

        public string Explanation { get; }
    }
}
