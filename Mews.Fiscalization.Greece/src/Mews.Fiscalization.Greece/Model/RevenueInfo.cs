using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Greece.Model
{
    public sealed class RevenueInfo
    {
        public RevenueInfo(TaxType taxType, RevenueType revenueType, VatExemptionType? vatExemption = null)
        {
            TaxType = taxType;
            RevenueType = revenueType;
            VatExemption = vatExemption.ToOption();
        }

        public TaxType TaxType { get; }

        public RevenueType RevenueType { get; }

        public IOption<VatExemptionType> VatExemption { get; }

        public static ITry<RevenueInfo, Error> Create(TaxType taxType, RevenueType revenueType, VatExemptionType? vatExemptionType = null)
        {
            if (taxType == TaxType.Vat0 && !vatExemptionType.HasValue)
            {
                return Try.Error<RevenueInfo, Error>(new Error($"{nameof(VatExemption)} must be specified when TaxType is {taxType}"));
            }
            return Try.Success<RevenueInfo, Error>(new RevenueInfo(taxType, revenueType, vatExemptionType));
        }
    }
}
