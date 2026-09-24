using System.Net;
using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Hungary.Tests;

/// <summary>
/// A failed call has to come back as an error result the caller can read, whatever shape NAV's answer takes.
/// </summary>
public sealed class ClientErrorResponseTests
{
    [Test]
    public async Task GeneralErrorResponse_IsMappedToItsErrorCode()
    {
        var content = Serialize(new Dto.GeneralErrorResponse
        {
            result = new Dto.BasicResultType { funcCode = Dto.FunctionCodeType.ERROR, errorCode = "INVALID_REQUEST_SIGNATURE", message = "Bad signature." }
        });

        var result = await GetTransactionListAsync(HttpStatusCode.BadRequest, content);

        Assert.That(result.GeneralErrorResult.ErrorCode, Is.EqualTo(ResultErrorCode.InvalidSigningKey));
    }

    [Test]
    public async Task GeneralExceptionResponse_IsMappedRatherThanThrown()
    {
        // Spec 3.1.1: what NAV answers to a request it cannot process technically, such as one failing the
        // schema.
        var content = Serialize(new Dto.GeneralExceptionResponse { funcCode = Dto.FunctionCodeType.ERROR, errorCode = "INVALID_REQUEST", message = "Schema violation." });

        var result = await GetTransactionListAsync(HttpStatusCode.BadRequest, content);

        Assert.That(result.GeneralErrorResult.ErrorCode, Is.EqualTo(ResultErrorCode.InvalidRequest));
        Assert.That(result.GeneralErrorResult.Message, Is.EqualTo("Schema violation."));
    }

    [Test]
    public async Task NonXmlServerError_IsAnOperationFailureRatherThanThrown()
    {
        var result = await GetTransactionListAsync(HttpStatusCode.InternalServerError, "Internal Server Error");

        // NAV's own infrastructure failing, which is worth another attempt.
        Assert.That(result.GeneralErrorResult.ErrorCode, Is.EqualTo(ResultErrorCode.OperationFailed));
        Assert.That(result.ResponseXml, Is.EqualTo("Internal Server Error"));
    }

    [Test]
    public async Task RequestXml_DoesNotCarryTheTechnicalUsersCredentials()
    {
        // Callers keep this as the record of what was reported and show it to people.
        var result = await GetTransactionListAsync(HttpStatusCode.InternalServerError, "Internal Server Error");

        Assert.That(result.RequestXml, Does.Contain("requestId"));
        Assert.That(result.RequestXml, Does.Not.Contain("passwordHash"));
        Assert.That(result.RequestXml, Does.Not.Contain("requestSignature"));
        Assert.That(result.RequestXml, Does.Not.Contain(OfflineFixture.User.Login.Value));
    }

    [Test]
    public void GetRedactedRequestXml_ManageInvoiceRequest_DropsTheExchangeToken()
    {
        var request = new System.Xml.XmlDocument();
        request.LoadXml("<ManageInvoiceRequest><header><requestId>R1</requestId></header><user><login>secret</login></user><exchangeToken>token</exchangeToken><invoiceOperations/></ManageInvoiceRequest>");

        var redacted = Utils.Client.GetRedactedRequestXml(request.DocumentElement);

        Assert.That(redacted, Does.Contain("<requestId>R1</requestId>"));
        Assert.That(redacted, Does.Contain("<invoiceOperations />"));
        Assert.That(redacted, Does.Not.Contain("secret"));
        Assert.That(redacted, Does.Not.Contain("token"));
    }

    private static Task<ResponseResult<TransactionList, TransactionErrorCode>> GetTransactionListAsync(HttpStatusCode statusCode, string content)
    {
        var client = new NavClient(new HttpClient(new StubHandler(statusCode, content)), OfflineFixture.User, OfflineFixture.Software, NavEnvironment.Test);
        var toUtc = new DateTime(2026, 9, 24, 10, 0, 0, DateTimeKind.Utc);
        return client.GetTransactionListAsync(page: 1, insertedFromUtc: toUtc.AddMinutes(-10), insertedToUtc: toUtc);
    }

    private static string Serialize<T>(T dto)
        where T : class
    {
        return XmlSerializer.Serialize(dto, new XmlSerializationParameters(namespaces: ServiceInfo.XmlNamespace.ToEnumerable())).OuterXml;
    }

    private sealed class StubHandler(HttpStatusCode statusCode, string content) : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new HttpResponseMessage(statusCode) { Content = new StringContent(content) });
        }
    }
}
