﻿using FuncSharp;
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
                Region.Alaba, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_alaba_license") ?? "INSERT_LICENSE"),
                Region.Gipuzkoa, _ => String1To20.CreateUnsafe(System.Environment.GetEnvironmentVariable("basque_gipuzkoa_license") ?? "INSERT_LICENSE")
            ),
            name: String1To120.CreateUnsafe("Test"),
            version: String1To20.CreateUnsafe("1.0.0")
        );

        internal Issuer Issuer => new Issuer(name: Name.CreateUnsafe("Test issuing company"), nif: Region.Match(
            Region.Gipuzkoa, _ => LocalNif,
            Region.Alaba, _ => TaxpayerIdentificationNumber.Create(Countries.Spain, "A01111111").Success.Get() // For Alaba, the NIF must be registered in the region.
        ));

        internal static void AssertResponse(Region region, SendInvoiceResponse response)
        {
            var validationResults = response.ValidationResults.Flatten();

            // Alaba region validates that each invoice is chained, but that's something we can't do in tests, so we will be ignoring that error.
            var applicableValidationResults = region.Match(
                Region.Gipuzkoa, _ => validationResults,
                Region.Alaba, _ => validationResults.Where(r => !r.ErrorCode.Equals(ErrorCode.InvalidOrMissingInvoiceChain))
            );
            Assert.IsEmpty(applicableValidationResults);
            Assert.IsNotEmpty(response.QrCodeUri);
            Assert.IsNotEmpty(response.TBAIIdentifier);
            Assert.IsNotEmpty(response.XmlRequestContent);
            Assert.IsNotEmpty(response.XmlResponseContent);
            Assert.AreEqual(response.State, InvoiceState.Received);
        }
    }
}
