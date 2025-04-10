using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Sweden.Tests
{
    [TestFixture]
    public sealed class InfrasecTests
    {
        private static readonly X509Certificate2 TransactionSigningCertificate = new(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_signing_certificate_data") ?? "INSERT_INFRASEC_SIGNING_CERTIFICATE_DATA")
        );

        private static readonly X509Certificate2 EnrollmentSigningCertificate = new(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_enrollment_signing_certificate_data") ?? "INSERT_INFRASEC_ENROLLMENT_SIGNING_CERTIFICATE_DATA")
        );

        private static readonly X509Certificate2 TransactionCertificate = new(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_certificate_data") ?? "INSERT_INFRASEC_CERTIFICATE_DATA"),
            password: System.Environment.GetEnvironmentVariable("infrasec_certificate_password") ?? "INSERT_INFRASEC_CERTIFICATE_PASSWORD",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable
                : X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.Exportable
        );

        private static readonly X509Certificate2 EnrollmentCertificate = new(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_enrollment_certificate_data") ?? "INSERT_INFRASEC_ENROLLMENT_CERTIFICATE_DATA"),
            password: System.Environment.GetEnvironmentVariable("infrasec_enrollment_certificate_password") ?? "INSERT_INFRASEC_ENROLLMENT_CERTIFICATE_PASSWORD",
            RuntimeInformation.IsOSPlatform(OSPlatform.Windows)
                ? X509KeyStorageFlags.MachineKeySet | X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.Exportable
                : X509KeyStorageFlags.EphemeralKeySet | X509KeyStorageFlags.Exportable
        );

        private static readonly string OrganizationRegisterId = System.Environment.GetEnvironmentVariable("infrasec_register_id") ?? "INSERT_INFRASEC_REGISTER_ID";
        private static readonly string OrganizationNumber = System.Environment.GetEnvironmentVariable("infrasec_organization_number") ?? "INSERT_INFRASEC_ORGANIZATION_NUMBER";
        private static readonly string ApplicationId = System.Environment.GetEnvironmentVariable("infrasec_application_id") ?? "INSERT_INFRASEC_APPLICATION_ID";
        private static readonly string PartnerName = System.Environment.GetEnvironmentVariable("infrasec_partner_name") ?? "INSERT_INFRASEC_PARTNER_NAME";
        private static readonly string PartnerCode = System.Environment.GetEnvironmentVariable("infrasec_partner_code") ?? "INSERT_INFRASEC_PARTNER_CODE";
        private static readonly string RegisterMake = System.Environment.GetEnvironmentVariable("infrasec_register_make") ?? "INSERT_INFRASEC_REGISTER_MAKE";

        [Test]
        public async Task EnrollCcu_Succeeds_Async()
        {
            using (var store = new X509Store(StoreName.CertificateAuthority, StoreLocation.CurrentUser))
            {
                store.Open(OpenFlags.ReadWrite);
                store.Add(EnrollmentSigningCertificate);
                store.Add(EnrollmentCertificate);
                store.Close();
            }

            //test data taken from Infrasec php code sample
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
                partnerName: PartnerName,
                partnerCode: PartnerCode,
                registerMake: RegisterMake
            );

            var config = new InfrasecConfiguration(
                Environment: Environment.Test,
                TransactionCertificate: TransactionCertificate,
                EnrollmentCertificate: EnrollmentCertificate,
                TransactionSigningCertificates: [TransactionSigningCertificate],
                EnrollmentSigningCertificates: [EnrollmentSigningCertificate],
                NonEmptyString.CreateUnsafe("custombroker")
            );
            var client = new InfrasecClient(configuration: config);
            var result = await client.EnrollCcuAsync(data: enrollmentData, NonEmptyString.CreateUnsafe(ApplicationId));

            Assert.That(result.IsSuccess, Is.True);
            var successResult = result.Success.Get();
            Assert.That(successResult.Ccuid, Is.Not.Null.And.Not.Empty);
            Assert.That(successResult.RegisterId, Is.Not.Null.And.Not.Empty);
        }

        [Test]
        [TestCase(TransactionType.Sale, true)]
        [TestCase(TransactionType.Copy, true)]
        [TestCase(TransactionType.Proforma, true)]
        [TestCase(TransactionType.Sale, false)]
        [TestCase(TransactionType.Copy, false)]
        [TestCase(TransactionType.Proforma, false)]
        [Retry(2)]
        public async Task SendData_Succeeds_Async(TransactionType transactionType, bool isRefund)
        {
            var request = new TransactionData(
                dateTime: DateTime.Now,
                organizationNumber: long.Parse(OrganizationNumber),
                organizationRegisterId: OrganizationRegisterId,
                registerFullAddress: "Regeringsgatan 67 111 56 Stockholm",
                sequenceNumber: 123,
                transactionType: transactionType,
                twelvePercentTax: new TaxAmount(5.70m, 47.50m),
                twentyFivePercentTax: new TaxAmount(12.61m, 50.45m),
                saleAmount: isRefund ? null : 116.26m,
                refundAmount: isRefund ? 116.26m : null,
                copyDateTime: transactionType == TransactionType.Copy ? DateTime.Now : null,
                copySequenceNumber: transactionType == TransactionType.Copy ? 123 : null
            );
            var config = new InfrasecConfiguration(
                Environment: Environment.Test,
                TransactionCertificate: TransactionCertificate,
                EnrollmentCertificate: TransactionCertificate,
                TransactionSigningCertificates: [TransactionSigningCertificate],
                EnrollmentSigningCertificates: [],
                NonEmptyString.CreateUnsafe("custombroker")
            );
            var client = new InfrasecClient(config);

            var result = await client.SendTransactionAsync(request, NonEmptyString.CreateUnsafe("Test client"));

            Assert.That(result.IsSuccess, Is.True);
            var successResult = result.Success.Get();

            // Proforma and Copy transactions should have the same control code as the original transaction so its not returned in the response.
            if (transactionType is not (TransactionType.Copy or TransactionType.Proforma))
            {
                var controlCode = successResult.ControlCode.Get().ToString();
                Assert.That(controlCode, Is.Not.Null.And.Not.Empty);
            }
            Assert.That(successResult.ControlServerId, Is.Not.Null.And.Not.Empty);
        }
    }
}