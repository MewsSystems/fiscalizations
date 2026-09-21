using System.Xml;
using System.Xml.Schema;
using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Hungary.Tests;

/// <summary>
/// Validates the XML this library produces against the official NAV schemas. NAV rejects a schema invalid
/// data report asynchronously with SCHEMA_VIOLATION, so without this the first sign of a malformed element
/// is a failed fiscal record in production.
/// </summary>
public sealed class SchemaValidationTests
{
    private static readonly XmlSchemaSet Schemas = LoadSchemas();

    [Test]
    public void MapInvoice_NormalHufInvoice_IsSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.Create(InvoiceCategory.Normal).Success.Get()));
    }

    [Test]
    public void MapInvoice_AggregateForeignCurrencyInvoice_IsSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.CreateAggregateForeignCurrency().Success.Get()));
    }

    [Test]
    public void MapInvoice_PrivatePersonInvoice_IsSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.CreateForPrivatePerson().Success.Get()));
    }

    [Test]
    public void MapInvoice_ForeignCompanyWithCommunityVatNumber_IsSchemaValid()
    {
        // The reported production failure: '839817675' is not facet-valid against
        // CommunityVatNumberType's [A-Z]{2}[0-9A-Z]{2,13}.
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.CreateForEuCompany("DE811234567").Success.Get()));
    }

    [Test]
    public void MapInvoice_ExemptAndOutOfScopeLines_AreSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.CreateWithRates(
            VatRate.Percentage(0.27m).Success.Get(),
            VatRate.Exemption(TaxExemptionCase.Tam, "Tax exempt activity"),
            VatRate.OutOfScope(TaxOutOfScopeCase.Atk, "Tourist tax")
        ).Success.Get()));
    }

    [Test]
    public void MapInvoice_AdvanceItem_IsSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.CreateAdvance().Success.Get()));
    }

    [Test]
    public void MapInvoice_OwnUnitOfMeasure_IsSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapInvoice(InvoiceModelTestData.CreateWithUnit(UnitOfMeasure.Own("Night").Success.Get()).Success.Get()));
    }

    [Test]
    public void MapModificationInvoice_Storno_IsSchemaValid()
    {
        AssertSchemaValid(RequestMapper.MapModificationInvoice(InvoiceModelTestData.CreateStorno().Success.Get()));
    }

    [Test]
    public void MapInvoice_CommunityVatNumberWithoutItsCountryPrefix_IsRejectedBySchema()
    {
        // Guards the guard: this is the exact XML NAV rejected in production, so it proves these tests fail
        // when the data is wrong rather than passing whatever they are handed. The caller is responsible for
        // prefixing the number - Core's TaxpayerIdentificationNumber accepts it either way.
        var invoiceData = RequestMapper.MapInvoice(InvoiceModelTestData.CreateForEuCompany("811234567", countryCode: "DE").Success.Get());

        Assert.That(GetSchemaErrors(invoiceData), Is.Not.Empty);
    }

    private static void AssertSchemaValid(Dto.InvoiceData invoiceData)
    {
        var (errors, xml) = Validate(invoiceData);
        Assert.That(errors, Is.Empty, () => $"{string.Join(Environment.NewLine, errors)}{Environment.NewLine}{xml}");
    }

    private static IReadOnlyList<string> GetSchemaErrors(Dto.InvoiceData invoiceData)
    {
        return Validate(invoiceData).Errors;
    }

    private static (IReadOnlyList<string> Errors, string Xml) Validate(Dto.InvoiceData invoiceData)
    {
        var parameters = new XmlSerializationParameters(namespaces: ServiceInfo.XmlNamespace.ToEnumerable());
        var xml = XmlSerializer.Serialize(invoiceData, parameters).OuterXml;

        var errors = new List<string>();
        var settings = new XmlReaderSettings
        {
            ValidationType = ValidationType.Schema,
            Schemas = Schemas
        };
        settings.ValidationEventHandler += (_, e) => errors.Add(e.Message);

        using var reader = XmlReader.Create(new StringReader(xml), settings);
        while (reader.Read())
        {
        }

        return (errors, xml);
    }

    private static XmlSchemaSet LoadSchemas()
    {
        var directory = Path.Combine(TestContext.CurrentContext.TestDirectory, "Schemas");
        var schemas = new XmlSchemaSet();
        foreach (var file in new[] { "common.xsd", "invoiceBase.xsd", "invoiceData.xsd", "invoiceApi.xsd" })
        {
            schemas.Add(null, Path.Combine(directory, file));
        }
        schemas.Compile();
        return schemas;
    }
}
