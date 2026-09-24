namespace Mews.Fiscalizations.Hungary.Models;

/// <summary>
/// NAV vatRate - exactly one of the ways an invoice line or summary entry can state its VAT treatment.
/// <para>
/// Modelled as a closed set of factories rather than a nullable percentage because a line taxed at 0% and a
/// line that carries no VAT at all are different statements: the first is a vatPercentage of 0, the second
/// has to name the legal ground it carries no VAT on. The three branches Mews cannot substantiate -
/// vatContent (simplified invoices only), marginSchemeIndicator and vatAmountMismatch - are deliberately not
/// offered.
/// </para>
/// </summary>
public sealed class VatRate
{
    private VatRate(decimal? percentage, DetailedReason exemption, DetailedReason outOfScope, bool isDomesticReverseCharge, bool isNoVatCharge)
    {
        PercentageValue = percentage;
        ExemptionValue = exemption;
        OutOfScopeValue = outOfScope;
        IsDomesticReverseCharge = isDomesticReverseCharge;
        IsNoVatCharge = isNoVatCharge;
    }

    internal decimal? PercentageValue { get; }

    internal DetailedReason ExemptionValue { get; }

    internal DetailedReason OutOfScopeValue { get; }

    internal bool IsDomesticReverseCharge { get; }

    internal bool IsNoVatCharge { get; }

    public static Try<VatRate, Error> Percentage(decimal percentage)
    {
        return percentage.ToTry(
            condition: p => TaxationInfo.PercentageTaxRates.Contains(p),
            error: p => new Error($"VAT percentage {p} is not accepted by NAV for a normal or aggregate invoice.")
        ).Map(p => new VatRate(p, null, null, false, false));
    }

    public static VatRate Exemption(TaxExemptionCase exemptionCase, string reason)
    {
        return new VatRate(null, new DetailedReason(exemptionCase.ToString().ToUpperInvariant(), reason), null, false, false);
    }

    public static VatRate OutOfScope(TaxOutOfScopeCase outOfScopeCase, string reason)
    {
        return new VatRate(null, null, new DetailedReason(outOfScopeCase.ToString().ToUpperInvariant(), reason), false, false);
    }

    public static VatRate DomesticReverseCharge()
    {
        return new VatRate(null, null, null, true, false);
    }

    /// <summary>
    /// noVatCharge - no VAT charged under Section 17 of the VAT Act, meaning the transfer of a business as a
    /// going concern. This is a narrow legal case, not a catch-all for untaxed lines.
    /// </summary>
    public static VatRate NoVatCharge()
    {
        return new VatRate(null, null, null, false, true);
    }

    public T Match<T>(
        Func<decimal, T> percentage,
        Func<DetailedReason, T> exemption,
        Func<DetailedReason, T> outOfScope,
        Func<Unit, T> domesticReverseCharge,
        Func<Unit, T> noVatCharge)
    {
        if (PercentageValue.HasValue)
        {
            return percentage(PercentageValue.Value);
        }
        if (ExemptionValue is not null)
        {
            return exemption(ExemptionValue);
        }
        if (OutOfScopeValue is not null)
        {
            return outOfScope(OutOfScopeValue);
        }
        return IsDomesticReverseCharge ? domesticReverseCharge(Unit.Value) : noVatCharge(Unit.Value);
    }

    public override bool Equals(object obj)
    {
        return obj is VatRate other
            && PercentageValue == other.PercentageValue
            && Equals(ExemptionValue, other.ExemptionValue)
            && Equals(OutOfScopeValue, other.OutOfScopeValue)
            && IsDomesticReverseCharge == other.IsDomesticReverseCharge
            && IsNoVatCharge == other.IsNoVatCharge;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(PercentageValue, ExemptionValue, OutOfScopeValue, IsDomesticReverseCharge, IsNoVatCharge);
    }
}

/// <summary>
/// NAV DetailedReasonType - a case code plus its textual justification, used by vatExemption and
/// vatOutOfScope.
/// </summary>
public sealed class DetailedReason
{
    internal DetailedReason(string caseCode, string reason)
    {
        Case = caseCode;
        Reason = reason;
    }

    public string Case { get; }

    public string Reason { get; }

    public override bool Equals(object obj)
    {
        return obj is DetailedReason other && Case == other.Case && Reason == other.Reason;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Case, Reason);
    }
}
