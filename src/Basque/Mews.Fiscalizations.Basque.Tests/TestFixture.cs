using FuncSharp;
using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Basque.Tests
{
    public sealed class TestFixture
    {
        internal static readonly X509Certificate2 Certificate = new X509Certificate2(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("spanish_certificate_data") ?? "INSERT_CERTIFICATE_DATA"),
            password: System.Environment.GetEnvironmentVariable("spanish_certificate_password") ?? "INSERT_CERTIFICATE_PASSWORD",
            keyStorageFlags: X509KeyStorageFlags.DefaultKeySet
        );

        internal static readonly TaxpayerIdentificationNumber LocalNif = TaxpayerIdentificationNumber.Create(
            country: Countries.Spain,
            taxpayerNumber: System.Environment.GetEnvironmentVariable("spanish_issuer_tax_number") ?? "INSERT_TAX_ID"
        ).Success.Get();

        public TestFixture(Region region)
        {
            Region = region;
        }

        internal Region Region { get; }

        internal TicketBaiClient Client => new TicketBaiClient(Certificate, Region, Environment.Test);

        internal Software Software => Software.LocalSoftwareDeveloper(
            nif: LocalNif,
            license: Region.Match(
                Region.Araba, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_araba_license") ?? "INSERT_LICENSE"),
                Region.Gipuzkoa, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_gipuzkoa_license") ?? "INSERT_LICENSE")
            ),
            name: String1To120.CreateUnsafe("Test"),
            version: String1To20.CreateUnsafe("1.0.0")
        );

        internal Issuer Issuer => Issuer.Create(name: Name.CreateUnsafe("Test issuing company"), nif: Region.Match(
            Region.Gipuzkoa, _ => LocalNif.TaxpayerNumber,
            Region.Araba, _ => "A01111111" // For Araba, the NIF must be registered in the region.
        )).Success.Get();

        internal static void AssertResponse(Region region, SendInvoiceResponse response, TicketBaiInvoiceData tbaiInvoiceData)
        {
            var validationResults = response.ValidationResults.Flatten();

            // Araba region validates that each invoice is chained, but that's something we can't do in tests, so we will be ignoring that error.
            var applicableValidationResults = region.Match(
                Region.Gipuzkoa, _ => validationResults,
                Region.Araba, _ => validationResults.Where(r => !r.ErrorCode.Equals(ErrorCode.InvalidOrMissingInvoiceChain))
            );
            Assert.IsEmpty(applicableValidationResults, "Reporting invoice failed...", new
            {
                ErrorCode = applicableValidationResults.First().ErrorCode,
                Description = applicableValidationResults.First().Description,
                Explanation = applicableValidationResults.First().Explanation
            });
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
}
