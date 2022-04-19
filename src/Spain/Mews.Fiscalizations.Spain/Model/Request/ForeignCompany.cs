namespace Mews.Fiscalizations.Spain.Model.Request
{
    public class ForeignCompany
    {
        public ForeignCompany(Name name, ForeignCompanyTaxpayerNumber taxpayerNumber)
        {
            Name = name;
            TaxpayerNumber = taxpayerNumber;
        }

        public Name Name { get; }

        public ForeignCompanyTaxpayerNumber TaxpayerNumber { get; }

        public ResidenceCountryIdentificatorType IdentificatorType => ResidenceCountryIdentificatorType.NotSelected;
    }
}
