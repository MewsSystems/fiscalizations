using Mews.Fiscalization.Germany.Model;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Germany
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
            return await DeserializeAsync(httpResponse, successFunc);
        }

        private async Task<HttpResponseMessage> SendRequestAsync<TRequest>(HttpMethod method, string endpoint, TRequest request, AccessToken token)
            where TRequest : class
        {
            var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
            var requestMessage = new HttpRequestMessage(method, uri);

            if (request != null)
            {
                requestMessage.Content = new StringContent(JsonConvert.SerializeObject(request, Formatting.None), Encoding.UTF8, "application/json");
            }

            if (token != null)
            {
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
            }
            return await HttpClient.SendAsync(requestMessage).ConfigureAwait(continueOnCapturedContext: false);
        }

        private async Task<ResponseResult<TResult>> DeserializeAsync<TDto, TResult>(HttpResponseMessage response, Func<TDto, ResponseResult<TResult>> successFunc)
            where TDto : class
            where TResult : class
        {
            var content = await response.Content.ReadAsStringAsync();
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
