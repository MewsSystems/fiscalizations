using NUnit.Framework;
using FuncSharp;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Italy.Constants;
using Mews.Fiscalizations.Italy.Uniwix.Communication;
using Mews.Fiscalizations.Italy.Uniwix.Communication.Dto;
using Mews.Fiscalizations.Italy.Uniwix.Errors;

namespace Mews.Fiscalizations.Italy.Tests;

[TestFixture]
public sealed class UniwixClientTests
{
    private static readonly string Username = Environment.GetEnvironmentVariable("italian_username") ?? "INSERT_USERNAME";
    private static readonly string Password = Environment.GetEnvironmentVariable("italian_password") ?? "INSERT_PASSWORD";

    public static UniwixClient GetUniwixClient()
    {
        return new UniwixClient(new UniwixClientConfiguration(Username, Password));
    }

    [Test]
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
                Assert.That(r.FileId, Is.Not.Empty);
                Assert.That(r.Message, Is.Not.Empty);

                // In the testing environment, Uniwix keeps the records in Pending state.
                var invoiceStateResult = client.GetInvoiceStateAsync(r.FileId).Result;
                invoiceStateResult.Match(
                    stateResult => Assert.That(stateResult.SdiState, Is.EqualTo(SdiState.Pending)),
                    e => AssertFail(e)
                );
            },
            e => AssertFail(e)
        );
    }

    [Test]
    [Retry(3)]
    public async Task VerifyCredentialsSucceeds()
    {
        var client = GetUniwixClient();
        var result = await client.VerifyCredentialsAsync();
        result.Match(
            r => Assert.That(r),
            e => AssertFail(e)
        );
    }

    [Test]
    public async Task GetInvoiceStateWithInvalidFileIdReturnsCorrectErrorType()
    {
        var client = GetUniwixClient();
        var result = await client.GetInvoiceStateAsync("InvoiceThatDoesntExist");
        Assert.That(result.Error.Get().Type, Is.EqualTo(ErrorType.InvoiceNotFound));
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
                },
                ReceptionData = new[]
                {
                    new OrderData
                    {
                        DocumentId = invoiceNumber,
                        // cig
                        TenderCode = "A1B2C3D4E5",
                        // cup
                        ProjectCode = "A1B2C3D4E5F6G7H"
                    }
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
                Kind = TaxKind.ExcludedArticle15,
                NormativeReference = NormativeReference.GetByInvoiceLineKind(TaxKind.ExcludedArticle15),
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
                Kind = TaxKind.ExcludedArticle15
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
        Assert.Fail($"{errorResult.Message}: Type: {errorResult.Type}");
    }
}