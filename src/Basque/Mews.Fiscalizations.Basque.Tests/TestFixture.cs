using FuncSharp;
using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Basque.Tests
{
    public static class TestFixture
    {
        internal static readonly X509Certificate2 Certificate = new X509Certificate2(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("spanish_certificate_data") ?? "INSERT_CERTIFICATE_DATA"),
            password: System.Environment.GetEnvironmentVariable("spanish_certificate_password") ?? "INSERT_CERTIFICATE_PASSWORD",
            keyStorageFlags: X509KeyStorageFlags.DefaultKeySet
        );

        internal static readonly Issuer Issuer = new Issuer(
            name: Name.CreateUnsafe("Test issuing company"),
            nif: TaxpayerIdentificationNumber.Create(Countries.Spain, System.Environment.GetEnvironmentVariable("spanish_issuer_tax_number") ?? "INSERT_TAX_ID").Success.Get()
        );

        internal static readonly TicketBaiClient Client = new TicketBaiClient(Certificate, Environment.Test);

        internal static readonly Software Software = Software.LocalSoftwareDeveloper(
            nif: Issuer.Nif,
            license: String1To20.CreateUnsafe("TBAIARblKjHKdjl00391"),
            name: String1To120.CreateUnsafe("Test"),
            version: String1To20.CreateUnsafe("1.0.0")
        );

        internal static void AssertResponse(SendInvoiceResponse response)
        {
            Assert.IsEmpty(response.ValidationResults.Flatten());
            Assert.IsNotEmpty(response.QrCodeUri);
            Assert.IsNotEmpty(response.TBAIIdentifier);
            Assert.IsNotEmpty(response.XmlRequestContent);
            Assert.IsNotEmpty(response.XmlResponseContent);
            Assert.AreEqual(response.State, InvoiceState.Received);
        }
    }
}
