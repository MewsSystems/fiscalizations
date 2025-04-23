using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Verifactu.Models;

namespace Mews.Fiscalizations.Verifactu;

internal class InternalClient
{
    private readonly HttpClient _httpClient;
    private readonly Uri BaseUri;
    private const string RelativeApiUrl = "api/v1/";

    public InternalClient(HttpClient httpClient, FiskalyEnvironment environment)
    {
        _httpClient = httpClient;
        BaseUri = environment.Match(
            FiskalyEnvironment.Test, _ => new Uri("https://test.es.sign.fiskaly.com"),
            FiskalyEnvironment.Production, _ => new Uri("https://live.es.sign.fiskaly.com")
        );
    }

    internal async Task<Try<TResult, ErrorResult>> ProcessRequestAsync<TRequest, TDto, TResult>(
        HttpMethod method,
        string endpoint,
        TRequest request,
        Func<TDto, Try<TResult, ErrorResult>> successFunc,
        CancellationToken cancellationToken,
        AccessToken token = null)
        where TRequest : class
        where TDto : class
        where TResult : class
    {
        var httpResponse = await SendRequestAsync(method, endpoint, request, cancellationToken, token);
        return await DeserializeAsync(httpResponse, successFunc, cancellationToken);
    }

    internal async Task<Try<TResult, ErrorResult>> GetResponseAsync<TDto, TResult>(string endpoint, Func<TDto, Try<TResult, ErrorResult>> successFunc, AccessToken token, CancellationToken cancellationToken)
        where TDto : class
        where TResult : class
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
        var httpResponse = await _httpClient.GetAsync(uri, cancellationToken);
        return await DeserializeAsync(httpResponse, successFunc, cancellationToken);
    }

    private async Task<HttpResponseMessage> SendRequestAsync<TRequest>(HttpMethod method, string endpoint, TRequest request, CancellationToken cancellationToken, AccessToken token = null)
        where TRequest : class
    {
        var uri = new Uri(BaseUri, $"{RelativeApiUrl}{endpoint}");
        var requestMessage = new HttpRequestMessage(method, uri)
        {
            Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
        };

        if (token is not null)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Value);
        }
        return await _httpClient.SendAsync(requestMessage, cancellationToken);
    }

    private async Task<Try<TResult, ErrorResult>> DeserializeAsync<TDto, TResult>(HttpResponseMessage response, Func<TDto, Try<TResult, ErrorResult>> successFunc, CancellationToken cancellationToken)
        where TDto : class
        where TResult : class
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        if (response.IsSuccessStatusCode)
        {
            return successFunc(JsonSerializer.Deserialize<TDto>(content));
        }

        return Try.Error<TResult, ErrorResult>(JsonSerializer.Deserialize<DTOs.FiskalyErrorResponse>(content).FromDto());
    }
}