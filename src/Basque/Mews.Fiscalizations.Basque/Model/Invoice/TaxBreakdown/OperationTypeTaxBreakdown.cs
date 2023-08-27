using FuncSharp;
using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Basque.Model;

public sealed class OperationTypeTaxBreakdown
{
    private OperationTypeTaxBreakdown(TaxSummary serviceProvision = null, TaxSummary delivery = null)
    {
        ServiceProvision = serviceProvision.ToOption();
        Delivery = delivery.ToOption();
    }

    public Option<TaxSummary> ServiceProvision { get; }

    public Option<TaxSummary> Delivery { get; }

    public static Try<OperationTypeTaxBreakdown, Error> Create(TaxSummary serviceProvision = null, TaxSummary delivery = null)
    {
        return (serviceProvision is not null || delivery is not null).ToTry(
            t => new OperationTypeTaxBreakdown(serviceProvision, delivery),
            f => new Error("At least 1 tax summary must be provided.")
        );
    }
}