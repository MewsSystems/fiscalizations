using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Hungary.Models
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
