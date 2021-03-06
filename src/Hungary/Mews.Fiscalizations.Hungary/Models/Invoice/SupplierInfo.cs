using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Hungary.Models
{
    public sealed class SupplierInfo : Info
    {
        public SupplierInfo(TaxpayerIdentificationNumber taxpayerId, Name name, SimpleAddress address, VatCode vatCode)
            : base(taxpayerId, name, address)
        {
            VatCode = Check.IsNotNull(vatCode, nameof(vatCode));
        }

        public VatCode VatCode { get; }
    }
}
