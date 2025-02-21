using Mews.Fiscalizations.Germany.V2.Model;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Mews.Fiscalizations.Germany.V2;

internal class Client
{
    private static readonly Uri BaseUri = new Uri("https://kassensichv-middleware.fiskaly.com");
    private static readonly string RelativeApiUrl = "api/v2/";

    private static HttpClient HttpClient { get; }

    static Client()
    {
        HttpClient = new HttpClient();
    }

    internal async Task<ResponseResult<TResult>> ProcessRequestAsync<TRequest, TDto, TResult>(
        HttpMethod method,
        string endpoint,
        TRequest request,
        Func<TDto, ResponseResult<TResult>> successFunc,
        CancellationToken cancellationToken,
        AccessToken token = null)
        where TRequest : class
        where TDto : class
        where TResult : class
    {
        var httpResponse = await SendRequestAsync(method, endpoint, request, cancellationToken, token);
        return await DeserializeAsync(httpResponse, successFunc, cancellationToken);
    }

    internal async Task<ResponseResult<TResult>> GetResponseAsync<TDto, TResult>(string endpoint, Func<TDto, ResponseResult<TResult>> successFunc, AccessToken token, CancellationToken cancellationToken)
        where TDto : class
        where TResult : class
    {
        HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
        var httpResponse = await HttpClient.GetAsync(uri, cancellationToken);
        return await DeserializeAsync(httpResponse, successFunc, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendRequestAsync<TRequest>(HttpMethod method, string endpoint, TRequest request, CancellationToken cancellationToken, AccessToken token = null)
        where TRequest : class
    {
        var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
        var requestMessage = new HttpRequestMessage(method, uri)
        {
            Content = new StringContent(JsonConvert.SerializeObject(request, Formatting.None), Encoding.UTF8, "application/json")
        };

        if (token is not null)
        {
            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        }
        return await HttpClient.SendAsync(requestMessage, cancellationToken);
    }

    private async Task<ResponseResult<TResult>> DeserializeAsync<TDto, TResult>(HttpResponseMessage response, Func<TDto, ResponseResult<TResult>> successFunc, CancellationToken cancellationToken)
        where TDto : class
        where TResult : class
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            var dto = Try.Catch<TDto, JsonReaderException>(_ => JsonConvert.DeserializeObject<TDto>(content));
            return dto.Match(
                d => successFunc(d),
                e => new ResponseResult<TResult>(errorResult: ErrorResult.MapException(e, content))
            );
        }

        var result = Try.Catch<Dto.FiskalyErrorResponse, JsonReaderException>(_ => JsonConvert.DeserializeObject<Dto.FiskalyErrorResponse>(content));
        return result.Match(
            r => new ResponseResult<TResult>(errorResult: ErrorResult.Map(r)),
            e => new ResponseResult<TResult>(errorResult: ErrorResult.MapException(e, content))
        );
    }
}