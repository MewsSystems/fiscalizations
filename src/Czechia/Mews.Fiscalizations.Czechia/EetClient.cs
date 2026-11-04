using System.Text;
using Mews.Fiscalizations.Czechia.Communication;
using Mews.Fiscalizations.Czechia.Model;

namespace Mews.Fiscalizations.Czechia;

/// <summary>
/// Client for the EET 2.0 web service (data interface version 4).
/// </summary>
public sealed class EetClient
{
    private readonly HttpClient _httpClient;
    private readonly Certificate _certificate;
    private readonly Uri _endpoint;

    public EetClient(HttpClient httpClient, Certificate certificate, EetEnvironment environment)
        : this(httpClient, certificate, environment.GetEndpoint())
    {
    }

    /// <summary>
    /// Use a custom endpoint, e.g. when the tax authority publishes a new interface version URL.
    /// </summary>
    public EetClient(HttpClient httpClient, Certificate certificate, Uri endpoint)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
        _endpoint = endpoint ?? throw new ArgumentNullException(nameof(endpoint));
    }

    /// <summary>
    /// Sends the registered sale. Use <paramref name="isFirstAttempt"/> = false when re-sending a sale that was not acknowledged before.
    /// </summary>
    public async Task<SendRevenueResult> SendRevenueAsync(
        RevenueRecord record,
        bool isFirstAttempt = true,
        EetMode mode = EetMode.Operational,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(record);

        var messageUuid = Guid.NewGuid();
        var message = RevenueMessage.Sign(
            RevenueMessage.Create(record, messageUuid, DateTimeOffset.Now, isFirstAttempt, mode),
            _certificate
        );
        var requestXml = message.OuterXml;

        using var request = new HttpRequestMessage(HttpMethod.Post, _endpoint);
        request.Headers.Add("SOAPAction", Namespaces.SoapAction);
        request.Content = new StringContent(requestXml, new UTF8Encoding(encoderShouldEmitUTF8Identifier: false), "text/xml");

        using var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseXml = await response.Content.ReadAsStringAsync(cancellationToken);
        var globalTransactionId = response.Headers.TryGetValues("X-Global-Transaction-Id", out var values)
            ? values.FirstOption()
            : Option.Empty<string>();

        return ResponseParser.Parse(responseXml, messageUuid, requestXml, globalTransactionId);
    }
}
