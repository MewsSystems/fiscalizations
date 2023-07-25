using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class ForeignCompany
{
    public ForeignCompany(Name name, ForeignTaxpayerNumber taxpayerNumber)
    {
        Name = Check.IsNotNull(name, nameof(name));
        TaxpayerNumber = Check.IsNotNull(taxpayerNumber, nameof(taxpayerNumber));
    }

    public Name Name { get; }

    public ForeignTaxpayerNumber TaxpayerNumber { get; }

    public ResidenceCountryIdentificatorType IdentificatorType => ResidenceCountryIdentificatorType.OtherSupportingDocument;
}