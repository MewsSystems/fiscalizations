using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Mews.Fiscalizations.Basque.Tests;

public sealed class TestFixture
{
    private static readonly X509Certificate2 Certificate = new(
        Convert.FromBase64String("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")
    );

    public TestFixture(Region region)
    {
        Region = region;
        LocalNif = TaxpayerIdentificationNumber.Create(
            country: Countries.Spain,
            taxpayerNumber: Region.Match(
                Region.Gipuzkoa, _ => "A01111111",
                Region.Araba, _ => System.Environment.GetEnvironmentVariable("spanish_issuer_tax_number") ?? "INSERT_TAX_ID",
                Region.Bizkaia, _ => "A01111111"
            )
        ).Success.Get();
    }

    private Region Region { get; }

    private TaxpayerIdentificationNumber LocalNif { get;  }

    internal TicketBaiClient Client => new(Certificate, Region, Environment.Test);

    internal Software Software => Software.LocalSoftwareDeveloper(
        nif: LocalNif,
        license: Region.Match(
            Region.Araba, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_araba_license") ?? "INSERT_LICENSE"),
            Region.Gipuzkoa, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_gipuzkoa_license") ?? "INSERT_LICENSE"),
            Region.Bizkaia, _ => String1To20.CreateUnsafe("TBAIBI00000000PRUEBA")
        ),
        name: String1To120.CreateUnsafe("IZENPE S.A"),
        version: String1To20.CreateUnsafe("1.0.0")
    );

    internal Issuer Issuer => Issuer.Create(name: Name.CreateUnsafe("IZENPE S.A"), LocalNif.TaxpayerNumber).Success.Get();

    internal static void AssertResponse(Region region, SendInvoiceResponse response, TicketBaiInvoiceData tbaiInvoiceData)
    {
        var validationResults = response.ValidationResults.Flatten();

        // Araba region validates that each invoice is chained, but that's something we can't do in tests, so we will be ignoring that error.
        // Also the NIF must be registered in the Araba region.
        var applicableValidationResults = region.Match(
            Region.Araba, _ => validationResults.Where(r =>
                r.ErrorCode != ErrorCode.InvalidOrMissingInvoiceChain
                && r.ErrorCode != ErrorCode.IssuerNifMustBeRegisteredInArabaRegion
                && r.ErrorCode != ErrorCode.ArabaRegionTestCertificate
            ),
            _ => validationResults
        );
        Assert.That(applicableValidationResults, Is.Empty);
        Assert.That(response.QrCodeUri, Is.Not.Empty);
        Assert.That(response.TBAIIdentifier, Is.Not.Empty);
        Assert.That(response.XmlRequestContent, Is.Not.Empty);
        Assert.That(response.XmlResponseContent, Is.Not.Empty);

        Assert.That(response.QrCodeUri.Contains(HttpUtility.UrlEncode(response.TBAIIdentifier)!, StringComparison.InvariantCultureIgnoreCase), Is.True);
        Assert.That(response.State, Is.EqualTo(InvoiceState.Received));
        Assert.That(response.TBAIIdentifier, Is.EqualTo(tbaiInvoiceData.TbaiIdentifier));
        Assert.That(response.QrCodeUri, Is.EqualTo(tbaiInvoiceData.QrCodeUri));
    }
}