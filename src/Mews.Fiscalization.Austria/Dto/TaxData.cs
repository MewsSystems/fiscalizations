namespace Mews.Fiscalization.Austria.Dto
{
    public sealed class TaxData
    {
        public TaxData(
            CurrencyValue standartRate = null,
            CurrencyValue reducedRate = null,
            CurrencyValue lowerReducedRate = null,
            CurrencyValue zeroRate = null,
            CurrencyValue specialRate = null
        )
        {
            var fallback = new CurrencyValue(0);
            StandardRate = standartRate ?? fallback;
            ReducedRate = reducedRate ?? fallback;
            LowerReducedRate = lowerReducedRate ?? fallback;
            ZeroRate = zeroRate ?? fallback;
            SpecialRate = specialRate ?? fallback;
        }

        public CurrencyValue StandardRate { get; }

        public CurrencyValue ReducedRate { get; }

        public CurrencyValue LowerReducedRate { get; }

        public CurrencyValue ZeroRate { get; }

        public CurrencyValue SpecialRate { get; }

        public decimal Sum()
        {
            return StandardRate.Value + ReducedRate.Value + LowerReducedRate.Value + ZeroRate.Value + SpecialRate.Value;
        }
    }
}
