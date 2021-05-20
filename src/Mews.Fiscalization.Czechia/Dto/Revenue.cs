namespace Mews.Eet.Dto
{
    public class Revenue
    {
        public Revenue(CurrencyValue gross, DateTimeWithTimeZone accepted = null, CurrencyValue notTaxable = null, TaxRateItem lowerReducedTaxRate = null, TaxRateItem reducedTaxRate = null, TaxRateItem standardTaxRate = null, CurrencyValue travelServices = null, CurrencyValue deposit = null, CurrencyValue usedDeposit = null)
        {
            Accepted = accepted ?? DateTimeProvider.Now;
            Gross = gross;
            NotTaxable = notTaxable;
            LowerReducedTaxRate = lowerReducedTaxRate;
            ReducedTaxRate = reducedTaxRate;
            StandardTaxRate = standardTaxRate;
            TravelServices = travelServices;
            Deposit = deposit;
            UsedDeposit = usedDeposit;
        }

        public DateTimeWithTimeZone Accepted { get; }

        public CurrencyValue Gross { get; }

        public CurrencyValue NotTaxable { get; }

        public TaxRateItem LowerReducedTaxRate { get; }

        public TaxRateItem ReducedTaxRate { get; }

        public TaxRateItem StandardTaxRate { get; }

        public CurrencyValue TravelServices { get; }

        public CurrencyValue Deposit { get; }

        public CurrencyValue UsedDeposit { get; }
    }
}
