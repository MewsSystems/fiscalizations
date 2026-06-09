using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.DSFinVK;

[TestFixture]
public class GermanVatRateDsfinvkTests
{
    // The vat_definition_export_id values are fixed by the DSFinV-K spec and are part of the wire
    // contract; changing them lands closings in the wrong bucket.
    [TestCase(GermanVatRate.Standard, 1)]
    [TestCase(GermanVatRate.Reduced, 2)]
    [TestCase(GermanVatRate.AverageHigher, 3)]
    [TestCase(GermanVatRate.AverageLower, 4)]
    [TestCase(GermanVatRate.NotTaxable, 5)]
    [TestCase(GermanVatRate.TaxFree, 6)]
    [TestCase(GermanVatRate.NotDeterminable, 7)]
    public void ToVatDefinitionExportId_MapsToSpecValue(GermanVatRate rate, long expected)
    {
        Assert.That(rate.ToVatDefinitionExportId(), Is.EqualTo(expected));
    }
}
