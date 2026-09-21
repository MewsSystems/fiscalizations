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
    public void Percentage_ZeroRate_Fails()
    {
        // NAV rejects vatPercentage = 0 on a normal or aggregate invoice (INVALID_VAT_DATA); a zero rated
        // supply has to be declared as an exemption or as out of scope, naming the legal ground.
        Assert.That(VatRate.Percentage(0m).IsSuccess, Is.False);
    }

    [Test]
    public void Percentage_UnsupportedRate_Fails()
    {
        Assert.That(VatRate.Percentage(0.19m).IsSuccess, Is.False);
    }

    [Test]
    public void Exemption_CarriesCaseAndReason()
    {
        var rate = VatRate.Exemption(TaxExemptionCase.Tam, "Tax exempt activity");

        Assert.That(rate.Match(p => (string)null, e => e.Case, o => null, drc => null, nvc => null), Is.EqualTo("TAM"));
        Assert.That(rate.Match(p => (string)null, e => e.Reason, o => null, drc => null, nvc => null), Is.EqualTo("Tax exempt activity"));
    }

    [Test]
    public void OutOfScope_CarriesCaseAndReason()
    {
        var rate = VatRate.OutOfScope(TaxOutOfScopeCase.Atk, "Outside the scope of VAT");

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
        Assert.That(VatRate.Exemption(TaxExemptionCase.Tam, "x"), Is.Not.EqualTo(VatRate.Exemption(TaxExemptionCase.Aam, "x")));
    }

    [Test]
    public void Equality_PercentageAndExemption_AreNotEqual()
    {
        Assert.That(VatRate.Percentage(0.27m).Success.Get(), Is.Not.EqualTo(VatRate.Exemption(TaxExemptionCase.Tam, "x")));
    }
}
