﻿using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Hungary.Models;
using System;
using System.Net.Http;
using System.Text;
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
            var httpResponse = await SendRequestAsync(endpoint, request).ConfigureAwait(continueOnCapturedContext: false);
            return await DeserializeAsync(httpResponse, successFunc);
        }

        private async Task<HttpResponseMessage> SendRequestAsync<TRequest>(string endpoint, TRequest request)
            where TRequest : class
        {
            var xml = XmlSerializer.Serialize(request);
            var content = new StringContent(xml.OuterXml, Encoding.UTF8, "application/xml");
            var uri = new Uri(ServiceInfo.BaseUrls[Environment], $"{ServiceInfo.RelativeServiceUrl}{endpoint}");
            return await HttpClient.PostAsync(uri, content).ConfigureAwait(continueOnCapturedContext: false);
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