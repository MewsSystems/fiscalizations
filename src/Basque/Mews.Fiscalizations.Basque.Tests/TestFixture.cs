using FuncSharp;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Basque.Tests;

public sealed class TestFixture
{
    // https://www.izenpe.eus/contenidos/informacion/cas_izenpe/es_cas/adjuntos/Kit_certificados_ficticios_PRODUCCION_Izenpe.zip
    private static readonly X509Certificate2 Certificate = new X509Certificate2(
        rawData: Convert.FromBase64String("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"),
        password: "Iz3np32023"
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

    internal TicketBaiClient Client => new TicketBaiClient(Certificate, Region, Environment.Test);

    internal Software Software => Software.LocalSoftwareDeveloper(
        nif: LocalNif,
        license: Region.Match(
            Region.Araba, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_araba_license") ?? "INSERT_LICENSE"),
            Region.Gipuzkoa, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_gipuzkoa_license") ?? "INSERT_LICENSE"),
            Region.Bizkaia, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_bizkaia_license") ?? "TBAIBI00000000PRUEBA")
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
            Region.Araba, _ => validationResults.Where(r => !r.ErrorCode.Equals(ErrorCode.InvalidOrMissingInvoiceChain) && !r.ErrorCode.Equals(ErrorCode.IssuerNifMustBeRegisteredInArabaRegion)),
            _ => validationResults
        );
        Assert.IsEmpty(applicableValidationResults);
        Assert.IsNotEmpty(response.QrCodeUri);
        Assert.IsNotEmpty(response.TBAIIdentifier);
        Assert.IsNotEmpty(response.XmlRequestContent);
        Assert.IsNotEmpty(response.XmlResponseContent);

        Assert.IsTrue(response.QrCodeUri.Contains(response.TBAIIdentifier));
        Assert.AreEqual(response.State, InvoiceState.Received);
        Assert.AreEqual(response.TBAIIdentifier, tbaiInvoiceData.TbaiIdentifier);
        Assert.AreEqual(response.QrCodeUri, tbaiInvoiceData.QrCodeUri);
    }
}