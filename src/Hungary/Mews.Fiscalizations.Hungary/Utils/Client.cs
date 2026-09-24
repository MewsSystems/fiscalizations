using Mews.Fiscalizations.Core.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Hungary.Utils;

internal sealed class Client
{
    private readonly HttpClient _httpClient;

    internal Client(HttpClient httpClient, NavEnvironment environment)
    {
        _httpClient = httpClient;
        Environment = environment;
    }

    private NavEnvironment Environment { get; }

    internal async Task<ResponseResult<TResult, TCode>> ProcessRequestAsync<TRequest, TDto, TResult, TCode>(
        string endpoint,
        TRequest request,
        Func<TDto, string, string, ResponseResult<TResult, TCode>> successFunc,
        CancellationToken cancellationToken)
        where TRequest : class
        where TDto : class
        where TResult : class
        where TCode : struct
    {
        var parameters = new XmlSerializationParameters(namespaces: ServiceInfo.XmlNamespace.ToEnumerable());
        var xmlRequest = XmlSerializer.Serialize(request, parameters);

        var httpResponse = await SendRequestAsync(endpoint, xmlRequest, cancellationToken);
        return await DeserializeAsync(httpResponse, GetRedactedRequestXml(xmlRequest), successFunc, cancellationToken);
    }

    /// <summary>
    /// The request as it may be kept and shown to people. Callers store it as the record of what was reported, so
    /// it must not carry the technical user's credentials - the login, password hash and request signature in
    /// the user element - nor the exchange token, none of which say anything about the invoice.
    /// </summary>
    internal static string GetRedactedRequestXml(XmlElement request)
    {
        var redacted = (XmlElement)request.CloneNode(deep: true);
        var secrets = redacted.SelectNodes("//*[local-name()='user' or local-name()='exchangeToken']")!.Cast<XmlNode>().ToList();
        foreach (var node in secrets)
        {
            node.ParentNode!.RemoveChild(node);
        }
        return redacted.OuterXml;
    }

    private async Task<HttpResponseMessage> SendRequestAsync(string endpoint, XmlElement requestXml, CancellationToken cancellationToken)
    {
        var content = new StringContent(requestXml.OuterXml, ServiceInfo.Encoding, "application/xml");
        var uri = new Uri(ServiceInfo.BaseUrls[Environment], $"{ServiceInfo.RelativeServiceUrl}{endpoint}");
        return await _httpClient.PostAsync(uri, content, cancellationToken);
    }

    private async Task<ResponseResult<TResult, TCode>> DeserializeAsync<TDto, TResult, TCode>(
        HttpResponseMessage response,
        string requestXml,
        Func<TDto, string, string, ResponseResult<TResult, TCode>> successFunc,
        CancellationToken cancellationToken)
        where TDto : class
        where TResult : class
        where TCode : struct
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return successFunc(XmlSerializer.Deserialize<TDto>(content), requestXml, content);
        }

        return new ResponseResult<TResult, TCode>(requestXml: requestXml, responseXml: content, generalErrorMessage: MapGeneralError(response, content));
    }

    /// <summary>
    /// NAV answers most failures with GeneralErrorResponse, but a request that fails schema validation with
    /// GeneralExceptionResponse (spec 3.2), and an infrastructure failure may not answer with XML at all. None
    /// of those is allowed to throw: the caller has to be able to read what went wrong.
    /// </summary>
    private static ErrorResult<ResultErrorCode> MapGeneralError(HttpResponseMessage response, string content)
    {
        var rootElementName = GetRootElementName(content);
        if (rootElementName == nameof(Dto.GeneralErrorResponse))
        {
            return ErrorResult<ResultErrorCode>.Map(XmlSerializer.Deserialize<Dto.GeneralErrorResponse>(content).result);
        }
        if (rootElementName == nameof(Dto.GeneralExceptionResponse))
        {
            return ErrorResult<ResultErrorCode>.Map(XmlSerializer.Deserialize<Dto.GeneralExceptionResponse>(content));
        }
        return new ErrorResult<ResultErrorCode>(ResultErrorCode.Unknown, message: $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}");
    }

    private static string GetRootElementName(string content)
    {
        try
        {
            using var reader = XmlReader.Create(new StringReader(content), new XmlReaderSettings { DtdProcessing = DtdProcessing.Prohibit });
            return reader.MoveToContent() == XmlNodeType.Element ? reader.LocalName : null;
        }
        catch (XmlException)
        {
            return null;
        }
    }
}