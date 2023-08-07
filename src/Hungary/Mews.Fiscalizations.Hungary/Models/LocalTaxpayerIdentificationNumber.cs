using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class LocalTaxpayerIdentificationNumber
{
    private LocalTaxpayerIdentificationNumber(TaxpayerIdentificationNumber value)
    {
        Value = value;
    }

    public TaxpayerIdentificationNumber Value { get; }

    public static Try<LocalTaxpayerIdentificationNumber, Error> Create(string taxId)
    {
        return TaxpayerIdentificationNumber.Create(Countries.Hungary, taxId, isCountryCodePrefixAllowed: false).Map(n => new LocalTaxpayerIdentificationNumber(n));
    }
}