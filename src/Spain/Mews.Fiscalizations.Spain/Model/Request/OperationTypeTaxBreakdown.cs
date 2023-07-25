using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Spain.Model.Request;

public sealed class OperationTypeTaxBreakdown
{
    private OperationTypeTaxBreakdown(TaxSummary serviceProvision = null, TaxSummary delivery = null)
    {
        ServiceProvision = serviceProvision.ToOption();
        Delivery = delivery.ToOption();
    }

    public IOption<TaxSummary> ServiceProvision { get; }

    public IOption<TaxSummary> Delivery { get; }

    public static ITry<OperationTypeTaxBreakdown, Error> Create(TaxSummary serviceProvision = null, TaxSummary delivery = null)
    {
        return (serviceProvision.IsNotNull() || delivery.IsNotNull()).ToTry(
            t => new OperationTypeTaxBreakdown(serviceProvision, delivery),
            f => new Error("At least 1 tax summary must be provided.")
        );
    }
}