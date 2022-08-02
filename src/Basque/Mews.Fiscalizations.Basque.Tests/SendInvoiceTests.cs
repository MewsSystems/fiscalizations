using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Basque.Model;
using NUnit.Framework;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Basque.Tests
{
    [TestFixture]
    public class Tests
    {
        private static readonly Random Random = new Random();

        private static readonly X509Certificate2 Certificate = new X509Certificate2(
            rawData: Convert.FromBase64String(System.Environment.GetEnvironmentVariable("spanish_certificate_data") ?? "INSERT_CERTIFICATE_DATA"),
            password: System.Environment.GetEnvironmentVariable("spanish_certificate_password") ?? "INSERT_CERTIFICATE_PASSWORD",
            keyStorageFlags: X509KeyStorageFlags.DefaultKeySet
        );

        private static readonly Issuer Issuer = new Issuer(
            name: Name.CreateUnsafe("Test issuing company"),
            nif: TaxpayerIdentificationNumber.Create(Countries.Spain, System.Environment.GetEnvironmentVariable("spanish_issuer_tax_number") ?? "INSERT_TAX_ID").Success.Get()
        );

        private static readonly TicketBaiClient Client = new TicketBaiClient(Certificate, Environment.Test);

        [Test]
        public async Task SendSimpleInvoiceSucceeds()
        {
            var request = CreateInvoiceRequest();
            var response = await Client.SendInvoiceAsync(request);

            // TODO: Assert the Qr code uri content, make sure it contains all the required params/data.
            var validationResults = response.ValidationResults.Flatten();
            Assert.IsEmpty(validationResults, "Response contains validation errors.", new
            {
                Descriptions = String.Join(", ", validationResults.Select(r => r.Description)),
                ErrorCodes = String.Join(", ", validationResults.Select(r => r.ErrorCode)),
                Explanations = String.Join(", ", validationResults.Select(r => r.Explanation))
            });
            Assert.IsNotEmpty(response.QrCodeUri);
            Assert.IsNotEmpty(response.TBAIIdentifier);
            Assert.IsNotEmpty(response.XmlRequestContent);
        }

        private SendInvoiceRequest CreateInvoiceRequest()
        {
            return new SendInvoiceRequest(
                subject: CreateSubject(),
                invoice: CreateInvoice(),
                invoiceFooter: CreateInvoiceFooter()
            );
        }

        private InvoiceFooter CreateInvoiceFooter()
        {
            return new InvoiceFooter(Software.LocalSoftwareDeveloper(
                nif: Issuer.Nif,
                license: String1To20.CreateUnsafe("TBAIARblKjHKdjl00391"),
                name: String1To120.CreateUnsafe("Mews Test"),
                version: String1To20.CreateUnsafe("1.0.4")
            ));
        }

        private Invoice CreateInvoice()
        {
            return new Invoice(CreateHeader(), CreateInvoiceData(), CreateTaxBreakdown());
        }

        private TaxBreakdown CreateTaxBreakdown()
        {
            return new TaxBreakdown(TaxSummary.Create(taxed: CreateTaxRateSummary(21m, 73.86m).ToEnumerable().ToArray()).Success.Get());
        }

        private InvoiceData CreateInvoiceData()
        {
            return InvoiceData.Create(
                description: String1To250.CreateUnsafe("factura ejemplo TBAI"),
                items: CreateInvoiceItems(),
                totalAmount: 89.36m,
                taxModes: TaxMode.GeneralTaxRegimeActivity.ToEnumerable()
            ).Success.Get();
        }

        private INonEmptyEnumerable<InvoiceItem> CreateInvoiceItems()
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

        private InvoiceHeader CreateHeader()
        {
            return new InvoiceHeader(
                number: String1To20.CreateUnsafe(RandomString(20)),
                issueDate: DateTime.Now,
                series: String1To20.CreateUnsafe(RandomString(20))
            );
        }

        private Subject CreateSubject()
        {
            return Subject.Create(Issuer, CreateReceivers(), IssuerType.IssuedByThirdParty).Success.Get();
        }

        private INonEmptyEnumerable<Receiver> CreateReceivers()
        {
            return NonEmptyEnumerable.Create(new Receiver(new LocalReceiver(
                taxpayerIdentificationNumber: TaxpayerIdentificationNumber.Create(Countries.Spain, "11111111H").Success.Get(),
                name: Name.CreateUnsafe("AAA, BBBBB, CCCCCCCCC"),
                postalCode: PostalCode.CreateUnsafe("01001"),
                address: String1To250.CreateUnsafe("Eduardo Dato 1")
            )));
        }

        private TaxRateSummary CreateTaxRateSummary(decimal vat, decimal baseValue)
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