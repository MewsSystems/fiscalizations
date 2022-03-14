using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Germany.V2.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.V2
{
    internal class Client
    {
        private static readonly Uri BaseUri = new Uri("https://kassensichv-middleware.fiskaly.com");
        private static readonly string RelativeApiUrl = "api/v2/";

        private HttpClient HttpClient { get; }

        internal Client()
        {
            HttpClient = new HttpClient();
        }

        internal async Task<ResponseResult<TResult>> ProcessRequestAsync<TRequest, TDto, TResult>(HttpMethod method, string endpoint, TRequest request, Func<TDto, ResponseResult<TResult>> successFunc, AccessToken token = null)
            where TRequest : class
            where TDto : class
            where TResult : class
        {
            var httpResponse = await SendRequestAsync(method, endpoint, request, token);
            return await DeserializeAsync(httpResponse, successFunc);
        }

        internal async Task<ResponseResult<TResult>> GetResponseAsync<TDto, TResult>(string endpoint, Func<TDto, ResponseResult<TResult>> successFunc, AccessToken token)
            where TDto : class
            where TResult : class
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
            var httpResponse = await HttpClient.GetAsync(uri);
            return await DeserializeAsync(httpResponse, successFunc);
        }

        private Task<HttpResponseMessage> SendRequestAsync<TRequest>(HttpMethod method, string endpoint, TRequest request, AccessToken token = null)
            where TRequest : class
        {
            var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request, Formatting.None), Encoding.UTF8, "application/json")
            };

            if (token.IsNotNull())
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            return HttpClient.SendAsync(requestMessage);
        }

        private async Task<ResponseResult<TResult>> DeserializeAsync<TDto, TResult>(HttpResponseMessage response, Func<TDto, ResponseResult<TResult>> successFunc)
            where TDto : class
            where TResult : class
        {
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = Try.Catch<ResponseResult<TResult>, JsonReaderException>(_ => successFunc(JsonConvert.DeserializeObject<TDto>(content)));
                return result.Match(
                    r => r,
                    e => new ResponseResult<TResult>(errorResult: ErrorResult.MapException(e, content))
                );
            }
            else
            {
                var result = Try.Catch<Dto.FiskalyErrorResponse, JsonReaderException>(_ => JsonConvert.DeserializeObject<Dto.FiskalyErrorResponse>(content));
                return result.Match(
                    r => new ResponseResult<TResult>(errorResult: ErrorResult.Map(r)),
                    e => new ResponseResult<TResult>(errorResult: ErrorResult.MapException(e, content))
                );
            }
        }
    }
}