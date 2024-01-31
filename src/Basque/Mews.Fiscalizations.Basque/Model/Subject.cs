namespace Mews.Fiscalizations.Basque.Model;

public sealed class Subject
{
    public Subject(Issuer issuer, IEnumerable<Receiver> receivers = null, IssuerType? issuerType = null)
    {
        Issuer = issuer;
        Receivers = receivers.ToOption();
        MultipleReceivers = Receivers.Map(r => r.Count() > 1);
        IssuerType = issuerType.ToOption();
    }

    public Issuer Issuer { get; }

    public Option<IEnumerable<Receiver>> Receivers { get; }

    public Option<bool> MultipleReceivers { get; }

    public Option<IssuerType> IssuerType { get; }
}