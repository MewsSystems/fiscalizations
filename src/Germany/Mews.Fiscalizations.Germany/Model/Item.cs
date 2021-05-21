namespace Mews.Fiscalizations.Germany.Model
{
    public sealed class Item
    {
        public Item(decimal amount, VatRateType vatRateType)
        {
            Amount = amount;
            VatRateType = vatRateType;
        }

        public decimal Amount { get; }
        
        public VatRateType VatRateType { get; }
    }
}
