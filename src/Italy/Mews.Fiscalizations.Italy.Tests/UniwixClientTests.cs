using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Linq;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Italy.Constants;
using Mews.Fiscalizations.Italy.Uniwix.Communication;
using Mews.Fiscalizations.Italy.Uniwix.Communication.Dto;
using Mews.Fiscalizations.Italy.Uniwix.Errors;

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
        [Ignore("Ignored temporarliy to unblock other PRs.")]
        [Retry(3)]
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

            result.Match(
                r =>
                {
                    Assert.IsNotEmpty(r.FileId);
                    Assert.IsNotEmpty(r.Message);

                    // In the testing environment, Uniwix keeps the records in Pending state.
                    var invoiceStateResult = client.GetInvoiceStateAsync(r.FileId).Result;
                    invoiceStateResult.Match(
                        stateResult => Assert.AreEqual(stateResult.SdiState, SdiState.Pending),
                        e => AssertFail(e)
                    );
                },
                e => AssertFail(e)
            );
        }

        [Test]
        public async Task VerifyCredentialsSucceeds()
        {
            var client = GetUniwixClient();
            var result = await client.VerifyCredentialsAsync();
            result.Match(
                r => Assert.IsTrue(r),
                e => AssertFail(e)
            );
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

        private void AssertFail(ErrorResult errorResult)
        {
            Assert.Fail(errorResult.Message, new
            {
                Type = errorResult.Type.ToString()
            });
        }
    }
}
