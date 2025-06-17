using System.Security.Cryptography.X509Certificates;
using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Sweden.Tests;

[TestFixture]
public sealed class EnrollmentTests
{
    private const string RegisterMake = "Test";
    private const string ApplicationId = "Test";

    private static readonly X509Certificate2 EnrollmentCertificate = new(
        rawData: Convert.FromBase64String(Environment.GetEnvironmentVariable("infrasec_enrollment_certificate_data") ?? "ENROLLMENT_CERTIFICATE_DATA"),
        password: Environment.GetEnvironmentVariable("infrasec_enrollment_certificate_password")
    );

    private static readonly X509Certificate2 EnrollmentSigningCertificate = new(
        rawData: Convert.FromBase64String(Environment.GetEnvironmentVariable("infrasec_enrollment_signing_certificate_data") ?? "ENROLLMENT_SIGNING_CERTIFICATE_DATA"),
        password: Environment.GetEnvironmentVariable("infrasec_enrollment_signing_certificate_password")
    );

    private static readonly string PartnerCode = Environment.GetEnvironmentVariable("infrasec_partner_code") ?? "PARTNER_CODE";

    [Test]
    [Ignore("For local testing only.")]
    public async Task EnrollCcu_Succeeds_Async()
    {
        var enrollmentData = new NewEnrollmentData(
            chainName: "Hel world hote14",
            chainCode: "1335863911",
            integrationVersion: "1.0.0.0",
            storeName: "heo world 2",
            storeId: "01",
            storeAddressLine: "Testreet 36",
            storeZipCode: "373 73",
            storeCity: "TestCity",
            storeCompanyOrgNr: "133586-3911",
            storeCompanyName: "Hlo worl14",
            partnerName: "Mews",
            partnerCode: PartnerCode,
            registerMake: RegisterMake,
            localAlias: "Kassa 1"
        );

        var handler = new HttpClientHandler
        {
            ServerCertificateCustomValidationCallback = (_, _, _, _) => true
        };
        var certCollection = new X509Certificate2Collection
        {
            EnrollmentCertificate,
            //EnrollmentSigningCertificate
        };
        handler.ClientCertificates.AddRange(certCollection);

        var httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri("https://idm-verify.infrasec.se")
        };
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mews Test");

        var client = new InfrasecEnrollmentClient(httpClient);
        var result = await client.EnrollCcuAsync(data: enrollmentData, NonEmptyString.CreateUnsafe(ApplicationId));

        Assert.That(result.IsSuccess, Is.True);
        var successResult = result.Success.Get();
        Assert.That(successResult.TcsId, Is.Not.Null.And.Not.Empty);
        Assert.That(successResult.RegisterId, Is.Not.Null.And.Not.Empty);
    }
}