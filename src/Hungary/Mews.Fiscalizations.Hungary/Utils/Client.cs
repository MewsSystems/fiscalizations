using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Hungary.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Hungary.Utils
{
    internal class Client
    {
        private HttpClient HttpClient { get; }
        private NavEnvironment Environment { get; }

        internal Client(HttpClient httpClient, NavEnvironment environment)
        {
            HttpClient = httpClient;
            Environment = environment;
        }

        internal async Task<ResponseResult<TResult, TCode>> ProcessRequestAsync<TRequest, TDto, TResult, TCode>(string endpoint, TRequest request, Func<TDto, ResponseResult<TResult, TCode>> successFunc)
            where TRequest : class
            where TDto : class
            where TResult : class
            where TCode : struct
        {
            var httpResponse = await SendRequestAsync(endpoint, request);
            return await DeserializeAsync(httpResponse, successFunc);
        }

        private Task<HttpResponseMessage> SendRequestAsync<TRequest>(string endpoint, TRequest request)
            where TRequest : class
        {
            var parameters = new XmlSerializationParameters(namespaces: ServiceInfo.XmlNamespace.ToEnumerable());
            var xml = XmlSerializer.Serialize(request, parameters);
            var content = new StringContent(xml.OuterXml, ServiceInfo.Encoding, "application/xml");
            var uri = new Uri(ServiceInfo.BaseUrls[Environment], $"{ServiceInfo.RelativeServiceUrl}{endpoint}");
            return HttpClient.PostAsync(uri, content);
        }

        private async Task<ResponseResult<TResult, TCode>> DeserializeAsync<TDto, TResult, TCode>(HttpResponseMessage response, Func<TDto, ResponseResult<TResult, TCode>> successFunc)
            where TDto : class
            where TResult : class
            where TCode : struct
        {
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return successFunc(XmlSerializer.Deserialize<TDto>(content));
            }
            else
            {
                var errorResult = XmlSerializer.Deserialize<Dto.GeneralErrorResponse>(content);
                return new ResponseResult<TResult, TCode>(generalErrorMessage: ErrorResult<TCode>.Map(errorResult));
            }
        }
    }
}