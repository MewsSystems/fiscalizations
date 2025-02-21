using Mews.Fiscalizations.Austria.ATrust;
using Mews.Fiscalizations.Austria.Dto;
using Mews.Fiscalizations.Austria.Dto.Identifiers;

namespace Mews.Fiscalizations.Austria.Tests;

public class ATrustSignerTests
{
    public static readonly ATrustUserIdentifier UserId = new ATrustUserIdentifier(Environment.GetEnvironmentVariable("austrian_user_id") ?? "INSERT_USER_ID");
    public static readonly string Password = Environment.GetEnvironmentVariable("austrian_password") ?? "INSERT_PASSWORD";

    private static ATrustCredentials Credentials
    {
        get { return new ATrustCredentials(user: UserId, password: Password); }
    }

    [Test]
    [Retry(3)]
    public async Task ATrustSignerWorks()
    {
        var isWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);
        var europeTimeZone = "Central Europe Standard Time";
        var austrianTimeZone = TimeZoneInfo.FindSystemTimeZoneById(isWindows ? europeTimeZone : TZConvert.WindowsToIana(europeTimeZone));
        var austrianCulture = CultureInfo.GetCultureInfo("de-AT");
        var httpClient = new HttpClient();
        var signer = new ATrustSigner(httpClient, Credentials, ATrustEnvironment.Test);
        var info = await signer.GetCertificateInfoAsync();
        var result = await signer.SignAsync(new QrData(
            culture: austrianCulture,
            timeZone: austrianTimeZone,
            receipt: new Receipt(
                number: new ReceiptNumber("83469"),
                registerIdentifier: new RegisterIdentifier("DEMO-CASH-BOX817"),
                taxData: new TaxData(
                    standartRate: new CurrencyValue(29.73m),
                    lowerReducedRate: new CurrencyValue(36.41m),
                    specialRate: new CurrencyValue(21.19m)
                ),
                turnover: new CurrencyValue(0.0m),
                certificateSerialNumber: new CertificateSerialNumber(info.CertificateSerialNumberHex),
                key: AesKeyGenerator.GetNext(),
                created: new LocalDateTime(new DateTime(2015, 11, 25, 19, 20, 11), austrianTimeZone)
            )
        ));
        Assert.That(result, Is.Not.Null);
        Assert.That(result.JwsRepresentation, Is.Not.Null);
        Assert.That(result.SignedQrData, Is.Not.Null);
        Assert.That(result.JwsRepresentation.Value, Is.Not.Null);
        Assert.That(result.JwsRepresentation.Signature.Value, Is.Not.Null);
        Assert.That(result.SignedQrData.Value, Is.Not.Null);
        Assert.That(result.SignedQrData.Data.Value, Is.Not.Null);
    }

    [Test]
    public async Task GetCertificateInfoWorks()
    {
        var httpClient = new HttpClient();
        var info = await new ATrustSigner(httpClient, Credentials, ATrustEnvironment.Test).GetCertificateInfoAsync();
        Assert.That(info, Is.Not.Null);
        Assert.That(info.Certificate, Is.Not.Null);
        Assert.That(info.CertificateSerialNumber, Is.Not.Null);
        Assert.That(info.Algorithm, Is.Not.Null);
        Assert.That(info.CertificateSerialNumberHex, Is.Not.Null);
    }
}