using FuncSharp;

namespace Mews.Fiscalizations.Core.Model;

public class TaxpayerIdentificationNumber : Coproduct2<EuropeanUnionTaxpayerIdentificationNumber, NonEuropeanUnionTaxpayerIdentificationNumber>
{
    public TaxpayerIdentificationNumber(EuropeanUnionTaxpayerIdentificationNumber firstValue)
        : base(firstValue)
    {
    }

    public TaxpayerIdentificationNumber(NonEuropeanUnionTaxpayerIdentificationNumber secondValue)
        : base(secondValue)
    {
    }

    public string TaxpayerNumber
    {
        get
        {
            return Match(
                europeanUnionCountry => europeanUnionCountry.TaxpayerNumber,
                nonEuropeanUnionCountry => nonEuropeanUnionCountry.TaxpayerNumber
            );
        }
    }

    public Country Country
    {
        get
        {
            return Match(
                europeanUnionCountry => new Country(europeanUnionCountry.Country),
                nonEuropeanUnionCountry => new Country(nonEuropeanUnionCountry.Country)
            );
        }
    }

    public static ITry<TaxpayerIdentificationNumber, Error> Create(Country country, string taxpayerNumber, bool isCountryCodePrefixAllowed = true)
    {
        return ObjectValidations.NotNull(country).FlatMap(c => c.Match(
            europeanUnionCountry => EuropeanUnionTaxpayerIdentificationNumber.Create(europeanUnionCountry, taxpayerNumber, isCountryCodePrefixAllowed).Map(n => new TaxpayerIdentificationNumber(n)),
            nonEuropeanUnionCountry => NonEuropeanUnionTaxpayerIdentificationNumber.Create(nonEuropeanUnionCountry, taxpayerNumber).Map(n => new TaxpayerIdentificationNumber(n))
        ));
    }
}