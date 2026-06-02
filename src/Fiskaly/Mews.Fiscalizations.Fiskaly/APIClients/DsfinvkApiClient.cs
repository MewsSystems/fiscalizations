using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK;
using Mews.Fiscalizations.Fiskaly.DTOs.DSFinVK.Auth;
using Mews.Fiscalizations.Fiskaly.Mappers.DSFinVK.Auth;
using Mews.Fiscalizations.Fiskaly.Models;

namespace Mews.Fiscalizations.Fiskaly.APIClients;

public class DsfinvkApiClient(HttpClient httpClient, string apiKey, string apiSecret)
{
    // DSFinV-K exposes a single endpoint; the environment (test vs. production) is encoded in the API key itself
    // and surfaced back on the access token claims.
    private readonly Uri _baseUri = new("https://dsfinvk.fiskaly.com");

    private const string RelativeApiUrl = "api/v1/";
    private const string AuthEndpoint = "auth";

    private const string AuthSchema = "Bearer";
    private const string JsonContentType = "application/json";

    public async Task<ResponseResult<AccessToken>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var request = new AuthorizationTokenRequest
        {
            ApiKey = apiKey,
            ApiSecret = apiSecret
        };

        return await ProcessRequestAsync<AuthorizationTokenResponse, AccessToken>(
            method: HttpMethod.Post,
            endpoint: AuthEndpoint,
            request: new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType),
            successFunc: r => r.MapAuthResponse(),
            cancellationToken: cancellationToken
        );
    }

    private async Task<ResponseResult<TResult>> ProcessRequestAsync<TDto, TResult>(
        HttpMethod method,
        string endpoint,
        StringContent request,
        Func<TDto, TResult> successFunc,
        CancellationToken cancellationToken,
        AccessToken token = null)
        where TDto : class
        where TResult : class
    {
        var uri = new Uri(_baseUri, $"{RelativeApiUrl}{endpoint}");
        using var requestMessage = new HttpRequestMessage(method, uri);

        if (method != HttpMethod.Get && method != HttpMethod.Delete)
        {
            requestMessage.Content = request;
        }

        if (token is not null)
        {
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue(AuthSchema, token.Value);
        }

        using var httpResponse = await httpClient.SendAsync(requestMessage, cancellationToken);
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        if (httpResponse.IsSuccessStatusCode)
        {
            var dto = string.IsNullOrWhiteSpace(content) ? null : JsonSerializer.Deserialize<TDto>(content);
            return new ResponseResult<TResult>(successResult: successFunc(dto));
        }

        var error = string.IsNullOrWhiteSpace(content) ? null : JsonSerializer.Deserialize<DsfinvkErrorResponse>(content);
        return new ResponseResult<TResult>(errorResult: new ErrorResult(
            Status: error?.StatusCode ?? (int)httpResponse.StatusCode,
            Code: null,
            Error: error?.Error,
            Message: error?.Message
        ));
    }
}
