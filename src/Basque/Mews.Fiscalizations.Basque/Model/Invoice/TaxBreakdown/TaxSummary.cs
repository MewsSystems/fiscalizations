﻿namespace Mews.Fiscalizations.Basque.Model;

public sealed class TaxSummary
{
    private TaxSummary(INonEmptyEnumerable<TaxExemptItem> taxExempt = null, INonEmptyEnumerable<TaxRateSummary> taxed = null)
    {
        TaxExempt = taxExempt.ToOption();
        Taxed = taxed.ToOption();
    }

    public Option<INonEmptyEnumerable<TaxExemptItem>> TaxExempt { get; }

    public Option<INonEmptyEnumerable<TaxRateSummary>> Taxed { get; }

    public static Try<TaxSummary, IReadOnlyList<Error>> Create(TaxExemptItem[] taxExempt = null, TaxRateSummary[] taxed = null)
    {
        if (taxExempt.IsEmptyOrNull() && taxed.IsEmptyOrNull())
        {
            return Try.Error<TaxSummary, IReadOnlyList<Error>>(new Error("Tax summary must contain at least one item.").ToEnumerable());
        }

        var validTaxExemptItems = taxExempt.ToOption().FlatMap(e => e.AsNonEmpty()).Match(
            items => items.ToTry(i => i.Count <= 7, _ => new Error("There can only be up to 7 tax exempt items on one invoice.")),
            _ => Try.Success<INonEmptyEnumerable<TaxExemptItem>, Error>(null)
        );
        var validTaxRateSummaries = taxed.ToOption().FlatMap(t => t.AsNonEmpty()).Match(
            summaries => summaries.ToTry(s => s.Count <= 6, _ => new Error("There can only be up to 6 distinct taxed items on one invoice.")),
            _ => Try.Success<INonEmptyEnumerable<TaxRateSummary>, Error>(null)
        );

        return Try.Aggregate(
            validTaxExemptItems,
            validTaxRateSummaries,
            (i, s) => new TaxSummary(taxExempt: i, taxed: s)
        );
    }
}