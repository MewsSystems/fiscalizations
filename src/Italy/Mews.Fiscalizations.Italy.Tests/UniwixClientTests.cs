using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Linq;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Italy.Constants;
using System.Threading;
using Mews.Fiscalizations.Italy.Uniwix.Communication;
using Mews.Fiscalizations.Italy.Uniwix.Communication.Dto;

namespace Mews.Fiscalizations.Italy.Tests
{
    [TestFixture]
    public sealed class UniwixClientTests
    {
        public static readonly string Username = Environment.GetEnvironmentVariable("italian_username") ?? "INSERT_USERNAME";
        public static readonly string Password = Environment.GetEnvironmentVariable("italian_password") ?? "INSERT_PASSWORD";

        public static UniwixClient GetUniwixClient()
        {
            return new UniwixClient(new UniwixClientConfiguration(Username, Password));
        }

        [Test]
        public async Task SendInvoiceSucceeds()
        {
            var client = GetUniwixClient();
            var invoiceNumber = new Random().Next(1, 9999).ToString();
            var result = await client.SendInvoiceAsync(new ElectronicInvoice
            {
                Version = VersioneSchemaType.FPR12,
                Header = GetInvoiceHeader(invoiceNumber),
                Body = new[] { GetInvoiceBody(invoiceNumber) }
            });

            var fileId = result.FileId;
            Assert.IsNotEmpty(result.FileId);
            Assert.IsNotEmpty(result.Message);

            Thread.Sleep(500);

            var invoiceStateResult = await client.GetInvoiceStateAsync(fileId);

            // In the testing environment, Uniwix keeps the records in Pending state.
            Assert.AreEqual(invoiceStateResult.SdiState, SdiState.Pending);
        }

        [Test]
        public async Task VerifyCredentialsSucceeds()
        {
            var client = GetUniwixClient();
            Assert.IsTrue(await client.VerifyCredentialsAsync());
        }

        private ElectronicInvoiceHeader GetInvoiceHeader(string invoiceNumber)
        {
            return new ElectronicInvoiceHeader
            {
                TransmissionData = new TransmissionData
                {
                    SequentialNumber = invoiceNumber,
                    DestinationCode = "1234567",
                    TransmitterId = GetSenderId(),
                    TransmissionFormat = TransmissionFormat.FPR12,
                },
                Provider = new Provider
                {
                    IdentificationData = new IdentificationData
                    {
                        VatTaxId = GetSenderId(),
                        Identity = new Identity
                        {
                            CompanyName = "Italian company ltd."
                        },
                        FiscalRegime = FiscalRegime.Ordinary
                    },
                    OfficeAddress = GetAddress()
                },
                Buyer = new Buyer
                {
                    IdentityData = new SimpleIdentityData
                    {
                        Identity = new Identity
                        {
                            FirstName = "John",
                            LastName = "Smith"
                        },
                        TaxCode = "SDASDA96L27H501H"
                    },
                    OfficeAddress = GetAddress()
                }
            };
        }

        private ElectronicInvoiceBody GetInvoiceBody(string invoiceNumber)
        {
            var paymentData = new PaymentData
            {
                PaymentDetails = GetPaymentDetails().ToArray(),
                PaymentTerms = PaymentTerms.LumpSum
            };

            return new ElectronicInvoiceBody
            {
                GeneralData = new GeneralData
                {
                    GeneralDocumentData = new GeneralDocumentData
                    {
                        DocumentType = DocumentType.Invoice,
                        CurrencyCode = "EUR",
                        IssueDate = DateTime.UtcNow,
                        DocumentNumber = invoiceNumber,
                        TotalAmount = 100m
                    }
                },
                ServiceData = new ServiceData
                {
                    InvoiceLines = GetInvoiceLines().ToArray(),
                    TaxSummary = GetTaxRateSummaries().ToArray()
                },
                PaymentData = paymentData.ToEnumerable().ToArray()
            };
        }

        private INonEmptyEnumerable<TaxRateSummary> GetTaxRateSummaries()
        {
            return NonEmptyEnumerable.Create(
                new TaxRateSummary
                {
                    VatRate = 10m,
                    TaxAmount = 9m,
                    TaxableAmount = 90m,
                    VatDueDate = VatDueDate.Immediate
                },
                new TaxRateSummary
                {
                    Kind = TaxKind.ExcludingArticle15,
                    NormativeReference = NormativeReference.GetByInvoiceLineKind(TaxKind.ExcludingArticle15),
                    VatRate = 0m,
                    TaxAmount = 0m,
                    TaxableAmount = 1m,
                    VatDueDate = VatDueDate.Immediate
                }
            );
        }

        private INonEmptyEnumerable<InvoiceLine> GetInvoiceLines()
        {
            return NonEmptyEnumerable.Create(
                new InvoiceLine
                {
                    LineNumber = "2",
                    Description = "Item 1",
                    UnitCount = 2m,
                    PeriodStartingDate = DateTime.UtcNow,
                    PeriodClosingDate = DateTime.UtcNow,
                    UnitPrice = 0.5m,
                    TotalPrice = 1m,
                    VatRate = 0m,
                    Kind = TaxKind.ExcludingArticle15
                },
                new InvoiceLine
                {
                    LineNumber = "2",
                    Description = "Item 2",
                    UnitCount = 2m,
                    PeriodStartingDate = DateTime.UtcNow,
                    PeriodClosingDate = DateTime.UtcNow,
                    UnitPrice = -0.455m,
                    TotalPrice = -0.91m,
                    VatRate = 10m
                },
                new InvoiceLine
                {
                    LineNumber = "3",
                    Description = "Item 3",
                    UnitCount = 1m,
                    PeriodStartingDate = DateTime.UtcNow,
                    PeriodClosingDate = DateTime.UtcNow,
                    UnitPrice = 90.91m,
                    TotalPrice = 90.91m,
                    VatRate = 10m
                }
            );
        }

        private static INonEmptyEnumerable<PaymentDetail> GetPaymentDetails()
        {
            return NonEmptyEnumerable.Create(new PaymentDetail
            {
                PaymentMethod = PaymentMethod.Cash,
                PaymentAmount = 100m
            });
        }

        private static SenderId GetSenderId()
        {
            return new SenderId
            {
                CountryCode = Countries.Italy.Alpha2Code,
                TaxCode = "1234567"
            };
        }

        private static Address GetAddress()
        {
            return new Address
            {
                Street = "Roma Street",
                City = "Rome",
                CountryCode = Countries.Italy.Alpha2Code,
                ProvinceCode = "RM",
                Zip = "00031"
            };
        }
    }
}
