namespace Mews.Fiscalizations.Bizkaia.Model;

public sealed class Subject
{
    private Subject(Issuer issuer, IEnumerable<Receiver> receivers, IssuerType? issuerType = null)
    {
        Issuer = issuer;
        Receivers = receivers;
        MultipleReceivers = Receivers.Count() > 1;
        IssuerType = issuerType.ToOption();
    }

    public Issuer Issuer { get; }

    public IEnumerable<Receiver> Receivers { get; }

    public bool MultipleReceivers { get; }

    public Option<IssuerType> IssuerType { get; }

    public static Try<Subject, IReadOnlyList<Error>> Create(Issuer issuer, IEnumerable<Receiver> receivers, IssuerType? issuerType = null)
    {
        return Try.Aggregate(
            ObjectValidations.NotNull(issuer),
            receivers.ToList().ToTry(i => i.Any() && i.Count <= 100, _ => new Error($"{nameof(receivers)} count must be in range [1, 100].")),
            (i, r) => new Subject(i, r, issuerType)
        );
    }
}