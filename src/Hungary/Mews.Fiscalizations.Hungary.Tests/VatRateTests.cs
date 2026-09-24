namespace Mews.Fiscalizations.Hungary.Tests;

public sealed class VatRateTests
{
    [TestCase(0.05)]
    [TestCase(0.18)]
    [TestCase(0.27)]
    public void Percentage_AllowedRate_Succeeds(decimal rate)
    {
        Assert.That(VatRate.Percentage(rate).IsSuccess, Is.True);
    }

    [Test]
    public void Percentage_ZeroRate_Succeeds()
    {
        // NAV has accepted vatPercentage = 0 since interface version 3.24, for deliveries from 2024-01-01. A zero
        // rated supply is taxed at 0%, which is a different statement from an exemption.
        Assert.That(VatRate.Percentage(0m).IsSuccess, Is.True);
    }

    [Test]
    public void Percentage_UnsupportedRate_Fails()
    {
        Assert.That(VatRate.Percentage(0.19m).IsSuccess, Is.False);
    }

    [TestCase(0.2)]
    [TestCase(0.25)]
    public void Percentage_RateOnlyValidBefore2013_Fails(decimal rate)
    {
        // Spec 3.3.2 item 14: only for MODIFY/STORNO of, or a CREATE delivered before, 01/01/2013.
        Assert.That(VatRate.Percentage(rate).IsSuccess, Is.False);
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    public void Exemption_BlankReason_Fails(string reason)
    {
        // The schema's SimpleText200NotBlankType; a blank reason fails the whole data report.
        Assert.That(VatRate.Exemption(TaxExemptionCase.Tam, reason).IsSuccess, Is.False);
        Assert.That(VatRate.OutOfScope(TaxOutOfScopeCase.Atk, reason).IsSuccess, Is.False);
    }

    [Test]
    public void Exemption_ReasonLongerThan200Characters_Fails()
    {
        Assert.That(VatRate.Exemption(TaxExemptionCase.Tam, new string('x', 201)).IsSuccess, Is.False);
        Assert.That(VatRate.Exemption(TaxExemptionCase.Tam, new string('x', 200)).IsSuccess, Is.True);
    }

    [Test]
    public void Exemption_CarriesCaseAndReason()
    {
        var rate = VatRate.Exemption(TaxExemptionCase.Tam, "Tax exempt activity").Success.Get();

        Assert.That(rate.Match(p => (string)null, e => e.Case, o => null, drc => null, nvc => null), Is.EqualTo("TAM"));
        Assert.That(rate.Match(p => (string)null, e => e.Reason, o => null, drc => null, nvc => null), Is.EqualTo("Tax exempt activity"));
    }

    [Test]
    public void OutOfScope_CarriesCaseAndReason()
    {
        var rate = VatRate.OutOfScope(TaxOutOfScopeCase.Atk, "Outside the scope of VAT").Success.Get();

        Assert.That(rate.Match(p => (string)null, e => null, o => o.Case, drc => null, nvc => null), Is.EqualTo("ATK"));
    }

    [Test]
    public void Equality_SamePercentage_IsEqual()
    {
        // The invoice tax summary groups items by rate, so value equality is what keeps two 27% lines on one
        // summaryByVatRate entry.
        Assert.That(VatRate.Percentage(0.27m).Success.Get(), Is.EqualTo(VatRate.Percentage(0.27m).Success.Get()));
    }

    [Test]
    public void Equality_DifferentExemptionCase_IsNotEqual()
    {
        Assert.That(VatRate.Exemption(TaxExemptionCase.Tam, "x").Success.Get(), Is.Not.EqualTo(VatRate.Exemption(TaxExemptionCase.Aam, "x").Success.Get()));
    }

    [Test]
    public void Equality_PercentageAndExemption_AreNotEqual()
    {
        Assert.That(VatRate.Percentage(0.27m).Success.Get(), Is.Not.EqualTo(VatRate.Exemption(TaxExemptionCase.Tam, "x").Success.Get()));
    }
}
