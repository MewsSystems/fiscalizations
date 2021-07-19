namespace Mews.Fiscalizations.Germany.Model
{
    public sealed class Payment
    {
        public Payment(decimal amount, PaymentType type, string currencyCode)
        {
            Amount = amount;
            Type = type;
            CurrencyCode = currencyCode;
        }

        public decimal Amount { get; }

        public PaymentType Type { get; }

        public string CurrencyCode { get; }
    }
}
