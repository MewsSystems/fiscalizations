using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Fiskaly.DTOs.Management.Auth;
using Mews.Fiscalizations.Fiskaly.Mappers.Management.Auth;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES;
using Mews.Fiscalizations.Fiskaly.Models;

namespace Mews.Fiscalizations.Fiskaly.Clients;

public class ManagementApiClient(HttpClient httpClient, FiskalyEnvironment environment, string apiKey, string apiSecret)
{
    private const string BaseUrl = "https://dashboard.fiskaly.com/api/v0";

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
        var uri = new Uri($"{BaseUrl}{endpoint}");
        var requestMessage = new HttpRequestMessage(method, uri);
        
        if (method != HttpMethod.Get)
        {
            requestMessage.Content = request;
        }

        if (token is not null)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthSchema, token.Value);
        }
        var httpResponse = await httpClient.SendAsync(requestMessage, cancellationToken);
        
        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);
        
        return httpResponse.IsSuccessStatusCode ?
            new ResponseResult<TResult>(successResult: successFunc(JsonSerializer.Deserialize<TDto>(content))) :
            new ResponseResult<TResult>(errorResult: JsonSerializer.Deserialize<DTOs.FiskalyErrorResponse>(content).FromDto());
    }
}