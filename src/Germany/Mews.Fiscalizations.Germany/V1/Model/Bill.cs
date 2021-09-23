using System.Collections.Generic;

namespace Mews.Fiscalizations.Germany.V1.Model
{
    public enum PaymentType
    {
        Cash,
        NonCash
    }

    public enum BillType
    {
        Receipt,
        Invoice
    }

    public enum VatRateType
    {
        Normal,
        Reduced,
        SpecialRate1,
        SpecialRate2,
        None
    }

    public sealed class Bill
    {
        public Bill(BillType type, List<Payment> payments, List<Item> items)
        {
            Type = type;
            Payments = payments;
            Items = items;
        }

        public BillType Type { get; }

        public List<Payment> Payments { get; }

        public List<Item> Items { get; }
    }
}
