using Mews.Fiscalizations.Core.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Hungary.Utils;

internal class Client
{
    private HttpClient HttpClient { get; }
    private NavEnvironment Environment { get; }

    internal Client(HttpClient httpClient, NavEnvironment environment)
    {
        HttpClient = httpClient;
        Environment = environment;
    }

    internal async Task<ResponseResult<TResult, TCode>> ProcessRequestAsync<TRequest, TDto, TResult, TCode>(
        string endpoint,
        TRequest request,
        Func<TDto, string, string, ResponseResult<TResult, TCode>> successFunc)
        where TRequest : class
        where TDto : class
        where TResult : class
        where TCode : struct
    {
        var parameters = new XmlSerializationParameters(namespaces: ServiceInfo.XmlNamespace.ToEnumerable());
        var xmlRequest = XmlSerializer.Serialize(request, parameters);

        var httpResponse = await SendRequestAsync(endpoint, xmlRequest);
        return await DeserializeAsync(httpResponse, xmlRequest, successFunc);
    }

    private Task<HttpResponseMessage> SendRequestAsync(string endpoint, XmlElement requestXml)
    {
        var content = new StringContent(requestXml.OuterXml, ServiceInfo.Encoding, "application/xml");
        var uri = new Uri(ServiceInfo.BaseUrls[Environment], $"{ServiceInfo.RelativeServiceUrl}{endpoint}");
        return HttpClient.PostAsync(uri, content);
    }

    private async Task<ResponseResult<TResult, TCode>> DeserializeAsync<TDto, TResult, TCode>(
        HttpResponseMessage response,
        XmlElement xmlRequest,
        Func<TDto, string, string, ResponseResult<TResult, TCode>> successFunc)
        where TDto : class
        where TResult : class
        where TCode : struct
    {
        var content = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            return successFunc(XmlSerializer.Deserialize<TDto>(content), xmlRequest.OuterXml, content);
        }
        else
        {
            var errorResult = XmlSerializer.Deserialize<Dto.GeneralErrorResponse>(content);
            return new ResponseResult<TResult, TCode>(requestXml: xmlRequest.OuterXml, responseXml: content, generalErrorMessage: ErrorResult<TCode>.Map(errorResult));
        }
    }
}