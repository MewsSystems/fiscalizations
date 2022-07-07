namespace Mews.Fiscalizations.Basque.Model
{
    public sealed class CorrectingInvoiceAmount
    {
        public CorrectingInvoiceAmount(decimal amount, decimal fee, decimal surcharge)
        {
            Amount = amount;
            Fee = fee;
            Surcharge = surcharge;
        }

        public decimal Amount { get; }

        public decimal Fee { get; }

        public decimal Surcharge { get; }
    }
}