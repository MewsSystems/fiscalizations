using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Mews.Fiscalizations.Austria.ATrust;
using Mews.Fiscalizations.Austria.Dto;
using Mews.Fiscalizations.Austria.Dto.Identifiers;
using NUnit.Framework;
using TimeZoneConverter;

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
        var signer = new ATrustSigner(Credentials, ATrustEnvironment.Test);
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
        Assert.IsNotNull(result);
        Assert.IsNotNull(result.JwsRepresentation);
        Assert.IsNotNull(result.SignedQrData);
        Assert.IsNotEmpty(result.JwsRepresentation.Value);
        Assert.IsNotEmpty(result.JwsRepresentation.Signature.Value);
        Assert.IsNotEmpty(result.SignedQrData.Value);
        Assert.IsNotEmpty(result.SignedQrData.Data.Value);
    }

    [Test]
    public async Task GetCertificateInfoWorks()
    {
        var info = await new ATrustSigner(Credentials, ATrustEnvironment.Test).GetCertificateInfoAsync();
        Assert.IsNotNull(info);
        Assert.IsNotEmpty(info.Certificate);
        Assert.IsNotEmpty(info.CertificateSerialNumber);
        Assert.IsNotEmpty(info.Algorithm);
        Assert.IsNotEmpty(info.CertificateSerialNumberHex);
    }
}