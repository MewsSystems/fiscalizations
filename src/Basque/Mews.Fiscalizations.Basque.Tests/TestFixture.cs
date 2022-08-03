using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Core.Model;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace Mews.Fiscalizations.Basque.Tests
{
    public class TestFixture
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

        private static readonly Random Random = new Random();

        internal static SendInvoiceRequest CreateInvoiceRequest()
        {
            return new SendInvoiceRequest(
                subject: CreateSubject(),
                invoice: CreateInvoice(),
                invoiceFooter: CreateInvoiceFooter()
            );
        }

        private static InvoiceFooter CreateInvoiceFooter()
        {
            return new InvoiceFooter(Software.LocalSoftwareDeveloper(
                nif: Issuer.Nif,
                license: String1To20.CreateUnsafe("TBAIARblKjHKdjl00391"),
                name: String1To120.CreateUnsafe("Mews Test"),
                version: String1To20.CreateUnsafe("1.0.4")
            ));
        }

        private static Invoice CreateInvoice()
        {
            return new Invoice(CreateHeader(), CreateInvoiceData(), CreateTaxBreakdown());
        }

        private static TaxBreakdown CreateTaxBreakdown()
        {
            return new TaxBreakdown(TaxSummary.Create(taxed: CreateTaxRateSummary(21m, 73.86m).ToEnumerable().ToArray()).Success.Get());
        }

        private static InvoiceData CreateInvoiceData()
        {
            return InvoiceData.Create(
                description: String1To250.CreateUnsafe("factura ejemplo TBAI"),
                items: CreateInvoiceItems(),
                totalAmount: 89.36m,
                taxModes: TaxMode.GeneralTaxRegimeActivity.ToEnumerable()
            ).Success.Get();
        }

        private static INonEmptyEnumerable<InvoiceItem> CreateInvoiceItems()
        {
            return NonEmptyEnumerable.Create(
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Artículo 1 Ejemplo"),
                    quantity: 1,
                    unitAmount: 23.356m,
                    discount: 2.00m,
                    totalAmount: 25.84m
                ),
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Artículo 2 xxx"),
                    quantity: 1.50m,
                    unitAmount: 18.2m,
                    discount: 0m,
                    totalAmount: 33.03m
                ),
                new InvoiceItem(
                    description: String1To250.CreateUnsafe("Artículo 3 aaaaaaaa"),
                    quantity: 18,
                    unitAmount: 1.40m,
                    discount: 0m,
                    totalAmount: 30.49m
                )
            );
        }

        private static InvoiceHeader CreateHeader()
        {
            return new InvoiceHeader(
                number: String1To20.CreateUnsafe(RandomString(20)),
                issued: DateTime.Now,
                series: String1To20.CreateUnsafe(RandomString(20))
            );
        }

        private static Subject CreateSubject()
        {
            return Subject.Create(Issuer, CreateReceivers(), IssuerType.IssuedByThirdParty).Success.Get();
        }

        private static INonEmptyEnumerable<Receiver> CreateReceivers()
        {
            return NonEmptyEnumerable.Create(new Receiver(new LocalReceiver(
                taxpayerIdentificationNumber: TaxpayerIdentificationNumber.Create(Countries.Spain, "11111111H").Success.Get(),
                name: Name.CreateUnsafe("AAA, BBBBB, CCCCCCCCC"),
                postalCode: PostalCode.CreateUnsafe("01001"),
                address: String1To250.CreateUnsafe("Eduardo Dato 1")
            )));
        }

        private static TaxRateSummary CreateTaxRateSummary(decimal vat, decimal baseValue)
        {
            return new TaxRateSummary(
                taxRatePercentage: Percentage.Create(vat).Success.Get(),
                taxBaseAmount: Amount.Create(baseValue).Success.Get(),
                taxAmount: Amount.Create(Math.Round(baseValue * vat / 100, 2)).Success.Get()
            );
        }

        private static string RandomString(int length)
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
