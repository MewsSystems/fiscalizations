namespace Mews.Fiscalizations.Germany.V2.Model;

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