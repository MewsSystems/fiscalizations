using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Spain.Model.Request;
using NUnit.Framework;

namespace Mews.Fiscalizations.Spain.Tests;

[TestFixture]
public class CounterPartyTests
{
    private static readonly Name ValidName = Name.CreateUnsafe("Test Company");

    [TestCase("B12345678")]
    [TestCase("12345678A")]
    [TestCase("A1234567B")]
    public void ForeignCompanyWithSpanishNif_WithValidNif_Succeeds(string nif)
    {
        var result = CounterParty.ForeignCompanyWithSpanishNif(ValidName, nif);

        Assert.That(result.IsSuccess, Is.True);
        var counterParty = result.Success.Get();
        counterParty.Match(
            local =>
            {
                Assert.That(local.Name, Is.EqualTo(ValidName));
                Assert.That(local.TaxpayerIdentificationNumber.TaxpayerNumber, Is.EqualTo(nif));
            },
            foreign => Assert.Fail("Expected LocalCounterParty but got ForeignCounterParty.")
        );
    }

    [TestCase("")]
    [TestCase("INVALID")]
    [TestCase("123")]
    public void ForeignCompanyWithSpanishNif_WithInvalidNif_Fails(string nif)
    {
        var result = CounterParty.ForeignCompanyWithSpanishNif(ValidName, nif);

        Assert.That(result.IsSuccess, Is.False);
    }

    [TestCase("B12345678")]
    [TestCase("12345678A")]
    public void ForeignCompanyWithSpanishNif_ProducesSameResultAsLocal(string nif)
    {
        var fromLocal = CounterParty.Local(ValidName, nif);
        var fromForeignWithSpanishNif = CounterParty.ForeignCompanyWithSpanishNif(ValidName, nif);

        Assert.That(fromLocal.IsSuccess, Is.EqualTo(fromForeignWithSpanishNif.IsSuccess));

        var localCounterParty = fromLocal.Success.Get();
        var foreignWithNifCounterParty = fromForeignWithSpanishNif.Success.Get();

        localCounterParty.Match(
            local => foreignWithNifCounterParty.Match(
                otherLocal =>
                {
                    Assert.That(local.Name, Is.EqualTo(otherLocal.Name));
                    Assert.That(local.TaxpayerIdentificationNumber.TaxpayerNumber, Is.EqualTo(otherLocal.TaxpayerIdentificationNumber.TaxpayerNumber));
                },
                _ => Assert.Fail("Expected both to be LocalCounterParty.")
            ),
            _ => Assert.Fail("Expected LocalCounterParty.")
        );
    }
}
