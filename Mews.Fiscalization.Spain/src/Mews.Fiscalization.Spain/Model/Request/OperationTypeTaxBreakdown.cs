﻿using FuncSharp;
using Mews.Fiscalization.Core.Model;

namespace Mews.Fiscalization.Spain.Model.Request
{
    public sealed class OperationTypeTaxBreakdown
    {
        private OperationTypeTaxBreakdown(TaxSummary serviceProvision = null, TaxSummary delivery = null)
        {
            ServiceProvision = serviceProvision.ToOption();
            Delivery = delivery.ToOption();
        }

        public IOption<TaxSummary> ServiceProvision { get; }

        public IOption<TaxSummary> Delivery { get; }

        public static ITry<OperationTypeTaxBreakdown, INonEmptyEnumerable<Error>> Create(TaxSummary serviceProvision = null, TaxSummary delivery = null)
        {
            return (serviceProvision.IsNotNull() || delivery.IsNotNull()).ToTry(
                t => new OperationTypeTaxBreakdown(serviceProvision, delivery),
                f => Error.Create("At least 1 tax summary must be provided.")
            );
        }
    }
}
