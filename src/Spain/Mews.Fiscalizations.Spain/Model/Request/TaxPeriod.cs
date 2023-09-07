namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class TaxPeriod
{
    public TaxPeriod(Year year, Month month)
    {
        Year = Check.IsNotNull(year, nameof(year));
        Month = month;
    }

    public Year Year { get; }

    public Month Month { get; }
}