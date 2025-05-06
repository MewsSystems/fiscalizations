using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Fiskaly.DTOs.Management.Auth;
using Mews.Fiscalizations.Fiskaly.DTOs.Management.Organizations;
using Mews.Fiscalizations.Fiskaly.Mappers;
using Mews.Fiscalizations.Fiskaly.Mappers.Management.Auth;
using Mews.Fiscalizations.Fiskaly.Mappers.Management.Organizations;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.Management.Organizations;

namespace Mews.Fiscalizations.Fiskaly.APIClients;

public class ManagementApiClient(HttpClient httpClient, string apiKey, string apiSecret)
{
    private const string BaseUrl = "https://dashboard.fiskaly.com/api/v0";

    private const string AuthEndpoint = "auth";
    private const string OrganizationsEndpoint = "organizations";
    private const string ApiKeysEndpoint = "api-keys";
    private const string AuthSchema = "Bearer";
    private const string JsonContentType = "application/json";

    public async Task<ResponseResult<AccessToken>> AuthenticateAsync(CancellationToken cancellationToken = default)
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
    
    public async Task<ResponseResult<ManagedOrganization>> CreateManagedOrganizationAsync(
        AccessToken token,
        string name,
        string addressLine1,
        string town,
        string zip,
        string countryCode,
        Guid managedByOrganizationId,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateManagedOrganizationRequest
        {
            Name = name,
            AddressLine1 = addressLine1,
            Town = town,
            Zip = zip,
            CountryCode = countryCode,
            ManagedByOrganizationId = managedByOrganizationId.ToString() 
        };
        
        return await ProcessRequestAsync<CreateManagedOrganizationResponse, ManagedOrganization>(
            method: HttpMethod.Post,
            endpoint: OrganizationsEndpoint,
            request: new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType),
            successFunc: r => r.MapManagedOrganization(),
            cancellationToken: cancellationToken,
            token: token
        );
    }
    
    public async Task<ResponseResult<ManagedOrganizationApiKey>> CreateManagedOrganizationApiKeyAsync(
        AccessToken token,
        string apiKeyName,
        Guid organizationId,
        Guid managedByOrganizationId,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateManagedOrganizationApiKeyRequest
        {
            Name = apiKeyName,
            Status = ApiKeyStatusEnum.enabled,
            ManagedByOrganizationId = managedByOrganizationId.ToString()
        };

        return await ProcessRequestAsync<CreateManagedOrganizationApiKeyResponse, ResponseResult<ManagedOrganizationApiKey>>(
            method: HttpMethod.Post,
            endpoint: $"{OrganizationsEndpoint}/{organizationId}/{ApiKeysEndpoint}",
            request: new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType),
            successFunc: r => r.MapManagedOrganizationApiKey(),
            cancellationToken: cancellationToken,
            token: token
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
            new ResponseResult<TResult>(errorResult: JsonSerializer.Deserialize<DTOs.FiskalyErrorResponse>(content).MapErrorResponse());
    }
}