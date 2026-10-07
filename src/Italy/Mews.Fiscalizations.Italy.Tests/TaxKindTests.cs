using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Mews.Fiscalizations.Italy.Constants;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using NUnit.Framework;

namespace Mews.Fiscalizations.Italy.Tests;

[TestFixture]
public sealed class TaxKindTests
{
    [Test]
    public void EveryTaxKind_SerializesToAValidNaturaCode()
    {
        foreach (var field in typeof(TaxKind).GetFields(BindingFlags.Public | BindingFlags.Static))
        {
            var natura = field.GetCustomAttribute<XmlEnumAttribute>().Name;

            Assert.That(Regex.IsMatch(natura, @"^N[1-7](\.[0-9])?$"), Is.True, $"{field.Name} serializes to '{natura}'");
        }
    }

    [Test]
    public void EveryDetailedTaxKind_HasANormativeReference()
    {
        foreach (var taxKind in Enum.GetValues<TaxKind>().Except([TaxKind.NotSubject, TaxKind.NonTaxable]))
        {
            Assert.That(NormativeReference.GetByInvoiceLineKind(taxKind), Is.Not.Empty, taxKind.ToString());
        }
    }

    [Test]
    public void NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation_SerializesToN3_6()
    {
        var field = typeof(TaxKind).GetField(nameof(TaxKind.NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation));

        Assert.That(field.GetCustomAttribute<XmlEnumAttribute>().Name, Is.EqualTo("N3.6"));
    }
}
