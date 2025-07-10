using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Mews.Fiscalizations.Basque.Tests;

public sealed class TestFixture
{
    private static readonly X509Certificate2 Certificate = new(
        Path.Combine(AppContext.BaseDirectory, "EntitateOrdezkaria_RepresentanteDeEntidad.p12"),
        "IZDesa2025"
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