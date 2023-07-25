namespace Mews.Fiscalizations.Hungary.Models;

public sealed class LocalCompany
{
    public LocalCompany(LocalTaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address)
    {
        TaxpayerId = taxpayerId;
        Name = name;
        Address = address;
    }

    public LocalTaxpayerIdentificationNumber TaxpayerId { get; }

    public Name Name { get; }

    public SimpleAddress Address { get; }
}