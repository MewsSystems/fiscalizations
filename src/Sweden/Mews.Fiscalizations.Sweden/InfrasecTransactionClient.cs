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
    private const string _transactionApiEndpoint = "/tcsserver";

    public InfrasecTransactionClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Try<TransactionResponse, Exception>> SendTransactionAsync(TransactionData data, NonEmptyString applicationId, Guid? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToDto(applicationId:applicationId, requestId: requestId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var response = await _httpClient.PostAsync(_transactionApiEndpoint, new StringContent(xml.OuterXml, Encoding.UTF8, MediaTypeNames.Application.Xml), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<TransactionResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}, request: {xml.OuterXml}")
            );
        }
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var tcsResponse = Try.Catch<DTOs.TcsResponse, Exception>(
            _ => XmlSerializer.Deserialize<DTOs.TcsResponse>(responseContent)
        );
        return tcsResponse.Map(r => r.FromDto(xml.OuterXml, responseContent));
    }

    public async Task<Try<RegisterStatusResponse, Exception>> GetRegisterStatusAsync(RegisterStatusData data, NonEmptyString applicationId, Guid? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToDto(applicationId:applicationId, requestId: requestId);
        var xml = XmlSerializer.Serialize(request, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var response = await _httpClient.PostAsync(_transactionApiEndpoint, new StringContent(xml.OuterXml, Encoding.UTF8, MediaTypeNames.Application.Xml), cancellationToken);
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<RegisterStatusResponse, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}, request: {xml.OuterXml}")
            );
        }

        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var registerStatusResponse = Try.Catch<DTOs.RegisterStatusResponse, Exception>(
            _ => XmlSerializer.Deserialize<DTOs.RegisterStatusResponse>(responseContent)
        );
        return registerStatusResponse.Map(r => r.FromDto(xml.OuterXml, responseContent));
    }
}