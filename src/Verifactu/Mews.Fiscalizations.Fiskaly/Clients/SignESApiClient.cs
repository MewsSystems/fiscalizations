using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Auth;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Client;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signer;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayer;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES;
using Mews.Fiscalizations.Fiskaly.Models;
using AccessToken = Mews.Fiscalizations.Fiskaly.Models.AccessToken;
using Signer = Mews.Fiscalizations.Fiskaly.Models.Signer;
using TaxpayerState = Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayer.TaxpayerState;

namespace Mews.Fiscalizations.Fiskaly.Clients;

public class SignESApiClient(HttpClient httpClient, FiskalyEnvironment environment, string apiKey, string apiSecret)
{
    private readonly Uri _baseUri = environment switch
    {
        FiskalyEnvironment.Test => new Uri("https://test.es.sign.fiskaly.com"),
        FiskalyEnvironment.Production => new Uri("https://live.es.sign.fiskaly.com"),
        _ => throw new ArgumentOutOfRangeException(nameof(environment), environment, null)
    };
    
    private const string RelativeApiUrl = "api/v1/";
    private const string AuthEndpoint = "auth";
    private const string TaxpayerEndpoint = "taxpayer";
    private const string ClientsEndpoint = "clients";
    private const string SoftwareEndpoint = "software";
    private const string SignersEndpoint = "signers";

    private const string AuthSchema = "Bearer";
    private const string JsonContentType = "application/json";


    public async Task<ResponseResult<AccessToken>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var request = new AuthorizationTokenRequest
        {
            Content = new AuthorizationTokenRequestContent
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            }
        };

        return await ProcessRequestAsync<AuthorizationTokenResponse, AccessToken>(
            method: HttpMethod.Post,
            endpoint: AuthEndpoint,
            request: new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType),
            successFunc: r => r.FromDto(),
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Taxpayer>> CreateTaxpayerAsync(
        AccessToken token,
        string legalName,
        string taxIdentifier,
        TaxpayerTerritory territory,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateTaxpayer(legalName, taxIdentifier, territory)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Put,
            endpoint: TaxpayerEndpoint,
            request: requestBody,
            successFunc: r => r.FromDto(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Taxpayer>> GetTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Get,
            endpoint: TaxpayerEndpoint,
            request: null,
            successFunc: r => r.FromDto(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Taxpayer>> DisableTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new UpdateTaxpayerRequest
        {
            Data = new UpdateTaxpayerRequestData
            {
                State = TaxpayerState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Patch,
            endpoint: TaxpayerEndpoint,
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Signer>> CreateSignerAsync(AccessToken token, Guid? signerId = null, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(string.Empty);
        return await ProcessRequestAsync<SignerResponse, Signer>(
            method: HttpMethod.Put,
            endpoint: $"{SignersEndpoint}/{signerId ?? Guid.NewGuid()}",
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Signer>> GetSignerByIdAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<SignerResponse, Signer>(
            method: HttpMethod.Get,
            endpoint: $"{SignersEndpoint}/{signerId}",
            request: null,
            successFunc: r => r.FromDto(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<IEnumerable<Signer>>> GetAllSignersAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<MultipleSignerResponse, IEnumerable<Signer>>(
            method: HttpMethod.Get,
            endpoint: SignersEndpoint,
            request: null,
            successFunc: r => r.Results.Select(v => v.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Signer>> DisableSignerAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new UpdateSignerRequest
        {
            Data = new UpdateSignerRequestData
            {
                State = SignerState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<SignerResponse, Signer>(
            method: HttpMethod.Patch,
            endpoint: $"{SignersEndpoint}/{signerId}",
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<ClientDevice>> CreateClientAsync(AccessToken token, Guid? clientId = null, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(string.Empty);

        return await ProcessRequestAsync<ClientResponse, ClientDevice>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId ?? Guid.NewGuid()}",
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<ClientDevice>> GetClientByIdAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<ClientResponse, ClientDevice>(
            method: HttpMethod.Get,
            endpoint: $"{ClientsEndpoint}/{clientId}",
            request: null,
            successFunc: r => r.FromDto(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<IEnumerable<ClientDevice>>> GetAllClientsAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<MultipleClientResponse, IEnumerable<ClientDevice>>(
            method: HttpMethod.Get,
            endpoint: ClientsEndpoint,
            request: null,
            successFunc: r => r.Results.Select(v => v.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<ClientDevice>> DisableClientAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new UpdateClientRequest
        {
            Data = new UpdateClientRequestData
            {
                State = ClientState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<ClientResponse, ClientDevice>(
            method: HttpMethod.Patch,
            endpoint: $"{ClientsEndpoint}/{clientId}",
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<InvoiceResponse>> SendSimplifiedInvoiceAsync(
        AccessToken token,
        SimplifiedInvoice simplifiedInvoice,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateSimplifiedInvoice(simplifiedInvoice)), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<DTOs.SignES.Invoice.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<InvoiceResponse>> SendCompleteInvoiceAsync(
        AccessToken token,
        CompleteInvoice completeInvoice,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateCompleteInvoice(completeInvoice)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.SignES.Invoice.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.FromDto(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<SoftwareAuditData>> GetSoftwareAuditDataAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<SoftwareResponse, SoftwareAuditData>(
            method: HttpMethod.Get,
            endpoint: SoftwareEndpoint,
            request: null,
            successFunc: r => r.FromDto(),
            token: token,
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