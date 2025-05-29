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
        rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_enrollment_certificate_data") ?? "ENROLLMENT_CERTIFICATE_DATA"),
        password: System.Environment.GetEnvironmentVariable("infrasec_enrollment_certificate_password")
    );

    private static readonly X509Certificate2 EnrollmentSigningCertificate = new(
        rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_enrollment_signing_certificate_data") ?? "ENROLLMENT_SIGNING_CERTIFICATE_DATA"),
        password: System.Environment.GetEnvironmentVariable("infrasec_enrollment_signing_certificate_password")
    );

    private static readonly string PartnerCode = System.Environment.GetEnvironmentVariable("infrasec_partner_code") ?? "PARTNER_CODE";

    [Test]
    [Ignore("For local testing only.")]
    public async Task EnrollCcu_Succeeds_Async()
    {
        var enrollmentData = new NewEnrollmentData(
            chainName: "Godis Butikerna AB",
            chainCode: "37373",
            integrationVersion: "1.0.0.0",
            storeName: "Butik 37",
            storeId: "37",
            storeAddressLine: "Teststreet 36",
            storeZipCode: "373 73",
            storeCity: "TestCity",
            storeCompanyOrgNr: "373737-3737",
            storeCompanyName: "Butik 37",
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
            EnrollmentSigningCertificate
        };
        handler.ClientCertificates.AddRange(certCollection);

        var httpClient = new HttpClient(handler);
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mews Test");

        var client = new InfrasecEnrollmentClient(httpClient, Environment.Test);
        var result = await client.EnrollCcuAsync(data: enrollmentData, NonEmptyString.CreateUnsafe(ApplicationId));

        Assert.That(result.IsSuccess, Is.True);
        var successResult = result.Success.Get();
        Assert.That(successResult.Ccuid, Is.Not.Null.And.Not.Empty);
        Assert.That(successResult.RegisterId, Is.Not.Null.And.Not.Empty);
    }
}