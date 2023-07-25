using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models;

public sealed class SupplierInfo
{
    public SupplierInfo(LocalTaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address, VatCode vatCode)
    {
        TaxpayerId = Check.IsNotNull(taxpayerId, nameof(taxpayerId));
        Name = Check.IsNotNull(name, nameof(name));
        Address = Check.IsNotNull(address, nameof(address));
        VatCode = Check.IsNotNull(vatCode, nameof(vatCode));
    }

    public LocalTaxpayerIdentificationNumber TaxpayerId { get; }

    public Name Name { get; }

    public SimpleAddress Address { get; }

    public VatCode VatCode { get; }
}