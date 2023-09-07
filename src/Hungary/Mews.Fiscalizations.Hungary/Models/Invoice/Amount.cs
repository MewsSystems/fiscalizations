namespace Mews.Fiscalizations.Hungary.Models;

public sealed class Amount
{
    public Amount(AmountValue net, AmountValue gross, AmountValue tax)
    {
        Net = net;
        Gross = gross;
        Tax = tax;
    }

    public AmountValue Net { get; }

    public AmountValue Gross { get; }

    public AmountValue Tax { get; }

    internal static Amount Sum(IEnumerable<Amount> amounts)
    {
        var amountsList = amounts.ToList();
        return new Amount(
            net: new AmountValue(amountsList.Sum(a => a.Net.Value)),
            gross: new AmountValue(amountsList.Sum(a => a.Gross.Value)),
            tax: new AmountValue(amountsList.Sum(a => a.Tax.Value))
        );
    }
}