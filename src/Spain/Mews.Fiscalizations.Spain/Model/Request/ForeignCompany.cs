namespace Mews.Fiscalizations.Spain.Model.Request
{
    public class ForeignCompany
    {
        public ForeignCompany(
            Name name,
            ForeignCompayTaxpayerNumber taxpayerNumber,
            ResidenceCountryIdentificatorType identificatiorType = ResidenceCountryIdentificatorType.NotSelected)
        {
            Name = name;
            TaxpayerNumber = taxpayerNumber;
            IdentificatorType = identificatiorType;
        }

        public Name Name { get; }

        public ForeignCompayTaxpayerNumber TaxpayerNumber { get; }

        public ResidenceCountryIdentificatorType IdentificatorType { get; }
    }
}
