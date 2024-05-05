namespace Mews.Fiscalizations.Basque.Model;

public sealed class TaxRateSummary
{
    public TaxRateSummary(Percentage taxRatePercentage, Amount taxBaseAmount, Amount taxAmount, 
        Percentage surchargeRatePercentage, Amount surchargeAmount, Option<bool> isSurchargeOperation)
    {
        TaxRatePercentage = Check.IsNotNull(taxRatePercentage, nameof(taxRatePercentage));
        TaxBaseAmount = Check.IsNotNull(taxBaseAmount, nameof(taxBaseAmount));
        TaxAmount = Check.IsNotNull(taxAmount, nameof(taxAmount));
        SurchargeRatePercentage = surchargeRatePercentage;
        SurchargeAmount = surchargeAmount;
        IsSurchargeOperation = isSurchargeOperation;
    }

    public Percentage TaxRatePercentage { get; }

    public Amount TaxBaseAmount { get; }

    public Amount TaxAmount { get; }

    public Percentage SurchargeRatePercentage { get; }

    public Amount SurchargeAmount { get; }

    public Option<bool> IsSurchargeOperation { get; }

}