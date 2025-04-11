using System.Net.Http.Headers;
using System.Net.Mime;
using FuncSharp;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Sweden.Mappers;
using Mews.Fiscalizations.Sweden.Models;

namespace Mews.Fiscalizations.Sweden;

public sealed class InfrasecEnrollmentClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri _enrollmentApiUrl;
    private const string TestEnrollmentApiUrl = "https://idm-verify.infrasec.se/api/EnrollCCU.php";

    public InfrasecEnrollmentClient(InfrasecClientConfiguration configuration)
    {
        _enrollmentApiUrl = configuration.Environment switch
        {
            Environment.Test => new Uri(TestEnrollmentApiUrl),
            Environment.Production => throw new NotSupportedException("Production environment is not supported yet"),
            _ => throw new ArgumentOutOfRangeException(configuration.Environment.ToString())
        };
        _httpClient = HttpClientHelpers.CreateHttpClient(configuration.ClientCertificate, configuration.SigningCertificates.ToList(), configuration.UserAgent);
    }

    public async Task<Try<StatusEnrollmentResponse, Exception>> GetStatusCcuAsync(
        StatusEnrollmentData data,
        NonEmptyString applicationId,
        int? requestId = null,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToStatusDto(applicationId: applicationId, requestId: requestId);
        var requestContent = CreateRequest(request);
        var response = await _httpClient.PostAsync(_enrollmentApiUrl, requestContent.Value, cancellationToken);
        return await HandleResponse(response, requestContent.XmlString, (idmResponse, s) => idmResponse.FromStatusDto(s), cancellationToken);
    }

    public async Task<Try<NewEnrollmentResponse, Exception>> EnrollCcuAsync(NewEnrollmentData data, NonEmptyString applicationId, int? requestId = null, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var request = data.ToNewDto(applicationId: applicationId, requestId: requestId);
        var requestContent = CreateRequest(request);
        var response = await _httpClient.PostAsync(_enrollmentApiUrl, requestContent.Value, cancellationToken);
        return await HandleResponse(response, requestContent.XmlString, (idmResponse, s) => idmResponse.FromNewDto(s), cancellationToken);
    }

    private static async Task<Try<T, Exception>> HandleResponse<T>(
        HttpResponseMessage response,
        string xmlContent,
        Func<DTOs.IdmResponse, string, T> mapFunction,
        CancellationToken cancellationToken)
    {
        if (!response.IsSuccessStatusCode)
        {
            return Try.Error<T, Exception>(
                new Exception($"Request failed with status code: {response.StatusCode}, response: {await response.Content.ReadAsStringAsync(cancellationToken)}")
            );
        }
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);
        var enrollmentResponse = Try.Catch<DTOs.IdmResponse, Exception>(
            _ => XmlSerializer.Deserialize<DTOs.IdmResponse>(responseContent)
        );

        return enrollmentResponse.Map(r => mapFunction(r, xmlContent));
    }

    private static (StringContent Value, string XmlString) CreateRequest<T>(T dto)
        where T : class
    {
        var xml = XmlSerializer.Serialize(dto, new XmlSerializationParameters(namespaces: [new XmlNamespace("", "")]));
        var xmlString = xml.OuterXml;
        var stringContent = new StringContent(xmlString, new MediaTypeHeaderValue(MediaTypeNames.Application.Xml)
        {
            CharSet = string.Empty
        });
        return (stringContent, xmlString);
    }
}