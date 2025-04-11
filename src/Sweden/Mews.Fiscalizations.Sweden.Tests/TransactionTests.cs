using System.Security.Cryptography.X509Certificates;
using FuncSharp;
using Mews.Fiscalizations.Sweden.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Sweden.Tests
{
    [TestFixture]
    public sealed class TransactionTests
    {
        private static readonly X509Certificate2 TransactionSigningCertificate = new(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_signing_certificate_data") ?? "SIGNING_CERTIFICATE_DATA")
        );

        private static readonly X509Certificate2 TransactionCertificate = new(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("infrasec_certificate_data") ?? "CERTIFICATE_DATA"),
            password: System.Environment.GetEnvironmentVariable("infrasec_certificate_password") ?? "CERTIFICATE_PASSWORD"
        );

        private static readonly string OrganizationRegisterId = System.Environment.GetEnvironmentVariable("infrasec_register_id") ?? "REGISTER_ID";

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
                organizationNumber: 4444444444,
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
            var config = new InfrasecClientConfiguration(Environment.Test, TransactionCertificate, [TransactionSigningCertificate], NonEmptyString.CreateUnsafe("custombroker"));
            var client = new InfrasecTransactionClient(config);

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