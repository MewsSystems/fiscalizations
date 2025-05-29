using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Auth;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.ClientDevices;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Signers;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES.Auth;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES.ClientDevices;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES.Invoices;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES.Signers;
using Mews.Fiscalizations.Fiskaly.Mappers.SignES.Taxpayers;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.Models.SignES.ClientDevices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using AccessToken = Mews.Fiscalizations.Fiskaly.Models.AccessToken;
using Signer = Mews.Fiscalizations.Fiskaly.Models.SignES.Signers.Signer;
using TaxpayerState = Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers.TaxpayerState;

namespace Mews.Fiscalizations.Fiskaly.APIClients;

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
        var request = new ContentWrapper<AuthorizationTokenRequest>
        {
            Content = new AuthorizationTokenRequest
            {
                ApiKey = apiKey,
                ApiSecret = apiSecret
            }
        };

        return await ProcessRequestAsync<ContentWrapper<AuthorizationTokenResponse>, AccessToken>(
            method: HttpMethod.Post,
            endpoint: AuthEndpoint,
            request: new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, JsonContentType),
            successFunc: r => r.MapAuthResponse(),
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
        var requestBody = new StringContent(JsonSerializer.Serialize(TaxpayerMapper.MapTaxpayerRequest(legalName, taxIdentifier, territory)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<ContentWrapper<TaxpayerResponse>, Taxpayer>(
            method: HttpMethod.Put,
            endpoint: TaxpayerEndpoint,
            request: requestBody,
            successFunc: r => r.MapTaxpayerResponse(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Taxpayer>> GetTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<ContentWrapper<TaxpayerResponse>, Taxpayer>(
            method: HttpMethod.Get,
            endpoint: TaxpayerEndpoint,
            request: null,
            successFunc: r => r.MapTaxpayerResponse(),
            token: token,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<Taxpayer>> DisableTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new ContentWrapper<UpdateTaxpayerRequest>()
        {
            Content = new UpdateTaxpayerRequest
            {
                State = TaxpayerState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<ContentWrapper<TaxpayerResponse>, Taxpayer>(
            method: HttpMethod.Patch,
            endpoint: TaxpayerEndpoint,
            successFunc: r => r.MapTaxpayerResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Signer>> CreateSignerAsync(AccessToken token, Guid? signerId = null, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent("{ }", Encoding.UTF8, JsonContentType);
        return await ProcessRequestAsync<ContentWrapper<SignerDataResponse>, Signer>(
            method: HttpMethod.Put,
            endpoint: $"{SignersEndpoint}/{signerId ?? Guid.NewGuid()}",
            successFunc: r => r.MapSignerResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Signer>> GetSignerByIdAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<ContentWrapper<SignerDataResponse>, Signer>(
            method: HttpMethod.Get,
            endpoint: $"{SignersEndpoint}/{signerId}",
            request: null,
            successFunc: r => r.MapSignerResponse(),
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
            successFunc: r => r.Results.Select(v => v.MapSignerResponse()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Signer>> DisableSignerAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new ContentWrapper<UpdateSignerRequest>
        {
            Content = new UpdateSignerRequest
            {
                State = SignerState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<ContentWrapper<SignerDataResponse>, Signer>(
            method: HttpMethod.Patch,
            endpoint: $"{SignersEndpoint}/{signerId}",
            successFunc: r => r.MapSignerResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<ClientDevice>> CreateClientAsync(AccessToken token, Guid? clientId = null, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent("{ }", Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<ContentWrapper<ClientResponse>, ClientDevice>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId ?? Guid.NewGuid()}",
            successFunc: r => r.MapClientDeviceResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<ClientDevice>> GetClientByIdAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<ContentWrapper<ClientResponse>, ClientDevice>(
            method: HttpMethod.Get,
            endpoint: $"{ClientsEndpoint}/{clientId}",
            request: null,
            successFunc: r => r.MapClientDeviceResponse(),
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
            successFunc: r => r.Results.Select(v => v.MapClientDeviceResponse()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<ClientDevice>> DisableClientAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new ContentWrapper<UpdateClientRequest>
        {
            Content = new UpdateClientRequest
            {
                State = ClientState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<ContentWrapper<ClientResponse>, ClientDevice>(
            method: HttpMethod.Patch,
            endpoint: $"{ClientsEndpoint}/{clientId}",
            successFunc: r => r.MapClientDeviceResponse(),
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
        var requestBody = new StringContent(JsonSerializer.Serialize(InvoiceMapper.MapSimplifiedInvoiceRequest(simplifiedInvoice)), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<DTOs.SignES.Invoices.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.MapInvoiceResponse(),
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
        var requestBody = new StringContent(JsonSerializer.Serialize(InvoiceMapper.MapCompleteInvoiceRequest(completeInvoice)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.SignES.Invoices.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.MapInvoiceResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<InvoiceResponse>> SendCorrectingSimplifiedInvoiceAsync(
        AccessToken token,
        CorrectingSimplifiedInvoice invoice,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(InvoiceMapper.MapCorrectingSimplifiedInvoiceRequest(invoice)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.SignES.Invoices.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.MapInvoiceResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<InvoiceResponse>> SendCorrectingCompleteInvoiceAsync(
        AccessToken token,
        CorrectingCompleteInvoice invoice,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(InvoiceMapper.MapCorrectingCompleteInvoiceRequest(invoice)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.SignES.Invoices.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.MapInvoiceResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<InvoiceResponse>> GetInvoiceAsync(
        AccessToken token,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.SignES.Invoices.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Get,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.MapInvoiceResponse(),
            token: token,
            request: null,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<InvoiceResponse>> CancelInvoiceAsync(
        AccessToken token,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize("{ }"), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.SignES.Invoices.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Patch,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => r.MapInvoiceResponse(),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<ResponseResult<SoftwareAuditData>> GetSoftwareAuditDataAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<ContentWrapper<SoftwareDataResponse>, SoftwareAuditData>(
            method: HttpMethod.Get,
            endpoint: SoftwareEndpoint,
            request: null,
            successFunc: r => r.MapSoftwareAuditResponse(),
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
            new ResponseResult<TResult>(errorResult: JsonSerializer.Deserialize<ContentWrapper<SignESErrorResponse>>(content).Content.MapSignESErrorResponse());
    }
}