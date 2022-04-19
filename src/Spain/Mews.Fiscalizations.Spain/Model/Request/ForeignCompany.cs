namespace Mews.Fiscalizations.Spain.Model.Request
{
    public class ForeignCompany
    {
        public ForeignCompany(
            Name name,
            ForeignCompanyTaxpayerNumber taxpayerNumber,
            ResidenceCountryIdentificatorType identificatiorType = ResidenceCountryIdentificatorType.NotSelected)
        {
            Name = name;
            TaxpayerNumber = taxpayerNumber;
            IdentificatorType = identificatiorType;
        }

        public Name Name { get; }

        public ForeignCompanyTaxpayerNumber TaxpayerNumber { get; }

        public ResidenceCountryIdentificatorType IdentificatorType { get; }
    }
}
