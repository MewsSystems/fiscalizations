namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class CounterParty : Coproduct2<LocalCounterParty, ForeignCounterParty>
{
    public CounterParty(LocalCounterParty localCounterParty)
        : base(localCounterParty)
    {
        Check.IsNotNull(localCounterParty, nameof(localCounterParty));
    }

    public CounterParty(ForeignCounterParty foreignCounterParty)
        : base(foreignCounterParty)
    {
        Check.IsNotNull(foreignCounterParty, nameof(foreignCounterParty));
    }

    public static Try<CounterParty, Error> Local(Name name, string nifVat)
    {
        return LocalCounterParty.Create(name, nifVat).Map(p => new CounterParty(p));
    }

    public static CounterParty ForeignCustomer(Name name, ResidenceCountryIdentificatorType identificatiorType, String1To20 idNumber, Country country)
    {
        return new CounterParty(new ForeignCounterParty(new ForeignCustomer(name, identificatiorType, idNumber, country)));
    }

    public static CounterParty ForeignCompany(Name name, ForeignTaxpayerNumber taxpayerNumber)
    {
        return new CounterParty(new ForeignCounterParty(new ForeignCompany(name, taxpayerNumber)));
    }

    /// <summary>
    /// Creates a counterparty for a company based outside Spain that has been issued a Spanish NIF by the Tax Agency.
    /// The SII API identifies such entities by their NIF, same as domestic companies.
    /// </summary>
    public static Try<CounterParty, Error> ForeignCompanyWithSpanishNif(Name name, string spanishNif)
    {
        return Local(name, spanishNif);
    }
}