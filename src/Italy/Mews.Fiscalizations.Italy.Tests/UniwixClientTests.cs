using System.Net;
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

    private static UniwixClient GetUniwixClient()
    {
        var httpClient = new HttpClient();
        return new UniwixClient(httpClient, new UniwixClientConfiguration(Username, Password));
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

    [TestCase(HttpStatusCode.OK, "You are temporarily unavailable")]
    [TestCase(HttpStatusCode.OK, "{invalid")]
    public async Task SendInvoiceWithMalformedSuccessResponseReturnsUnknownError(HttpStatusCode statusCode, string body)
    {
        var result = await CreateClient(_ => CreateResponse(statusCode, body)).SendInvoiceAsync(CreateInvoice());

        Assert.That(result.IsError, Is.True);
        Assert.That(result.Error.Get().Type, Is.EqualTo(ErrorType.Unknown));
        Assert.That(result.Error.Get().Message, Does.Contain("non-JSON success response"));
    }

    [Test]
    public async Task SendInvoiceWithHtmlBadGatewayResponseReturnsErrorContainingStatusCode()
    {
        var result = await CreateClient(_ => CreateResponse(HttpStatusCode.BadGateway, "<html>Bad Gateway</html>")).SendInvoiceAsync(CreateInvoice());

        Assert.That(result.IsError, Is.True);
        Assert.That(result.Error.Get().Message, Does.Contain("502"));
    }

    [Test]
    public async Task SendInvoiceWhenRequestTimesOutReturnsConnectionError()
    {
        var result = await CreateClient(_ => throw new TaskCanceledException("The request timed out.")).SendInvoiceAsync(CreateInvoice());

        Assert.That(result.IsError, Is.True);
        Assert.That(result.Error.Get().Type, Is.EqualTo(ErrorType.Connection));
    }

    [Test]
    public async Task ConcurrentInvoiceUploadsIsolateMalformedResponseErrors()
    {
        var responseCount = 0;
        var client = CreateClient(_ => Interlocked.Increment(ref responseCount) == 1
            ? CreateResponse(HttpStatusCode.OK, "You are temporarily unavailable")
            : CreateResponse(HttpStatusCode.OK, "{\"code\":0,\"result\":{\"fid\":\"success\",\"msg\":\"Uploaded\"}}"));

        var results = await Task.WhenAll(
            client.SendInvoiceAsync(CreateInvoice("malformed")),
            client.SendInvoiceAsync(CreateInvoice("successful")));

        Assert.That(results[0].IsError, Is.True);
        Assert.That(results[1].IsSuccess, Is.True);
        Assert.That(results[1].Success.Get().FileId, Is.EqualTo("success"));
    }

    private static UniwixClient CreateClient(Func<HttpRequestMessage, HttpResponseMessage> responseFactory)
    {
        return new UniwixClient(
            new HttpClient(new StubHttpMessageHandler(responseFactory)),
            new UniwixClientConfiguration(Username, Password));
    }

    private static HttpResponseMessage CreateResponse(HttpStatusCode statusCode, string body)
    {
        return new HttpResponseMessage(statusCode) { Content = new StringContent(body) };
    }

    private ElectronicInvoice CreateInvoice(string invoiceNumber = "1")
    {
        return new ElectronicInvoice
        {
            Version = VersioneSchemaType.FPR12,
            Header = GetInvoiceHeader(invoiceNumber),
            Body = new[] { GetInvoiceBody(invoiceNumber) }
        };
    }

    private sealed class StubHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> responseFactory) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(responseFactory(request));
        }
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