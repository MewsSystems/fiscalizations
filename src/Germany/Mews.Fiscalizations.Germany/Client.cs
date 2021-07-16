using Mews.Fiscalizations.Germany.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany
{
    internal class Client
    {
        private static readonly Uri BaseUri = new Uri("https://kassensichv.io");
        private static readonly string RelativeApiUrl = "api/v1/";

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
            var httpResponse = await SendRequestAsync(method, endpoint, request, token).ConfigureAwait(continueOnCapturedContext: false);
            return await DeserializeAsync(httpResponse, successFunc).ConfigureAwait(continueOnCapturedContext: false);
        }

        internal async Task<ResponseResult<TResult>> GetResponseAsync<TDto, TResult>(string endpoint, Func<TDto, ResponseResult<TResult>> successFunc, AccessToken token)
            where TDto : class
            where TResult : class
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
            var httpResponse = await HttpClient.GetAsync(uri).ConfigureAwait(continueOnCapturedContext: false);
            return await DeserializeAsync(httpResponse, successFunc).ConfigureAwait(continueOnCapturedContext: false);
        }

        private Task<HttpResponseMessage> SendRequestAsync<TRequest>(HttpMethod method, string endpoint, TRequest request, AccessToken token)
            where TRequest : class
        {
            var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
            var requestMessage = new HttpRequestMessage(method, uri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request, Formatting.None), Encoding.UTF8, "application/json")
            };

            if (token != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            return HttpClient.SendAsync(requestMessage);
        }

        private async Task<ResponseResult<TResult>> DeserializeAsync<TDto, TResult>(HttpResponseMessage response, Func<TDto, ResponseResult<TResult>> successFunc)
            where TDto : class
            where TResult : class
        {
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(continueOnCapturedContext: false);
            if (response.IsSuccessStatusCode)
            {
                return successFunc(JsonConvert.DeserializeObject<TDto>(content));
            }
            else
            {
                var errorResult = JsonConvert.DeserializeObject<Dto.FiskalyErrorResponse>(content);
                return new ResponseResult<TResult>(errorResult: ErrorResult.Map(errorResult));
            }
        }
    }
}
