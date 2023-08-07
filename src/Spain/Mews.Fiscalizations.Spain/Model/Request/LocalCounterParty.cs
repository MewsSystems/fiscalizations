using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class LocalCounterParty
{
    private LocalCounterParty(Name name, TaxpayerIdentificationNumber taxpayerIdentificationNumber)
    {
        Name = Check.IsNotNull(name, nameof(name));
        TaxpayerIdentificationNumber = Check.IsNotNull(taxpayerIdentificationNumber, nameof(taxpayerIdentificationNumber));
    }

    public Name Name { get; }

    public TaxpayerIdentificationNumber TaxpayerIdentificationNumber { get; }

    public static Try<LocalCounterParty, Error> Create(Name name, string nifVat)
    {
        return TaxpayerIdentificationNumber.Create(Countries.Spain, nifVat).Map(n => new LocalCounterParty(name, n));
    }
}