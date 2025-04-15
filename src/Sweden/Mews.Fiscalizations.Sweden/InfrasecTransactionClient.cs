using System.Net.Mime;
using System.Text;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Sweden.Models;
using FuncSharp;
using Mews.Fiscalizations.Sweden.Mappers;

namespace Mews.Fiscalizations.Sweden;

public sealed class InfrasecTransactionClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _transactionApiUrl;
    private const string TestTransactionApiUrl = "https://tcs-verify.infrasec-api.se/tcsserver";

    public InfrasecTransactionClient(HttpClient httpClient, Environment environment)
    {
        _transactionApiUrl = environment switch
        {
            Environment.Test => new Uri(TestTransactionApiUrl),
            Environment.Production => throw new NotImplementedException("Production URL is not implemented"),
            _ => throw new ArgumentOutOfRangeException(environment.ToString())
        };
        _httpClient = httpClient;
    }

    public async Task<Try<TransactionResponse, Exception>> SendTransactionAsync(TransactionData data, NonEmptyString applicationId, Guid? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToDto(applicationId:applicationId, requestId: requestId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var response = await _httpClient.PostAsync(_transactionApiUrl, new StringContent(xml.OuterXml, Encoding.UTF8, MediaTypeNames.Application.Xml), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<TransactionResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}, request: {xml.OuterXml}")
            );
        }
        var tcsResponse = await Try.CatchAsync<DTOs.TcsResponse, Exception>(
            async _ => XmlSerializer.Deserialize<DTOs.TcsResponse>(await response.Content.ReadAsStringAsync(cancellationToken))
        );
        return tcsResponse.Map(r => r.FromDto(xml.OuterXml));
    }
}