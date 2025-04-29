using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Mews.Fiscalizations.Verifactu.Models;

namespace Mews.Fiscalizations.Verifactu;

public class SignESApiClient
{
    private readonly string _apiKey;
    private readonly string _apiSecret;
    private readonly HttpClient _httpClient;
    private readonly Uri _baseUri;
    
    private const string RelativeApiUrl = "api/v1/";
    private const string AuthEndpoint = "auth";
    private const string TaxpayerEndpoint = "taxpayer";
    private const string ClientsEndpoint = "clients";
    private const string SoftwareEndpoint = "software";
    private const string SignersEndpoint = "signers";

    private const string AuthSchema = "Bearer";
    private const string JsonContentType = "application/json";
    
    
    public SignESApiClient(HttpClient httpClient, FiskalyEnvironment environment, string apiKey, string apiSecret)
    {
        _apiKey = apiKey;
        _apiSecret = apiSecret;
        _httpClient = httpClient;
        _baseUri = environment.Match(
            FiskalyEnvironment.Test, _ => new Uri("https://test.es.sign.fiskaly.com"),
            FiskalyEnvironment.Production, _ => new Uri("https://live.es.sign.fiskaly.com")
        );
    }

    public async Task<Try<AccessToken, ErrorResult>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateAuthorizationToken(_apiKey, _apiSecret)), Encoding.UTF8, JsonContentType);
        return await ProcessRequestAsync<DTOs.AuthorizationTokenResponse, AccessToken>(
            method: HttpMethod.Post,
            endpoint: AuthEndpoint,
            request: requestBody,
            successFunc: r => Try.Success<AccessToken, ErrorResult>(r.FromDto()),
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Taxpayer, ErrorResult>> CreateTaxpayerAsync(
        AccessToken token,
        string legalName,
        string taxIdentifier,
        TaxpayerTerritory territory,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateTaxpayer(legalName, taxIdentifier, territory)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Put,
            endpoint: TaxpayerEndpoint,
            request: requestBody,
            successFunc: r => Try.Success<Taxpayer, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Taxpayer, ErrorResult>> GetTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Get,
            endpoint: TaxpayerEndpoint,
            request: null,
            successFunc: r => Try.Success<Taxpayer, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Taxpayer, ErrorResult>> DisableTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new DTOs.UpdateTaxpayerRequest
        {
            Data = new DTOs.UpdateTaxpayerRequestData
            {
                State = DTOs.TaxpayerState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Patch,
            endpoint: TaxpayerEndpoint,
            successFunc: r => Try.Success<Taxpayer, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Signer, ErrorResult>> CreateSignerAsync(AccessToken token, Guid? signerId = null, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(string.Empty);
        return await ProcessRequestAsync<DTOs.SignerResponse, Signer>(
            method: HttpMethod.Put,
            endpoint: $"{SignersEndpoint}/{signerId ?? Guid.NewGuid()}",
            successFunc: r => Try.Success<Signer, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Signer, ErrorResult>> GetSignerByIdAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.SignerResponse, Signer>(
            method: HttpMethod.Get,
            endpoint: $"{SignersEndpoint}/{signerId}",
            request: null,
            successFunc: r => Try.Success<Signer, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<IEnumerable<Signer>, ErrorResult>> GetAllSignersAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.MultipleSignerResponse, IEnumerable<Signer>>(
            method: HttpMethod.Get,
            endpoint: SignersEndpoint,
            request: null,
            successFunc: r => Try.Success<IEnumerable<Signer>, ErrorResult>(r.Results.Select(v => v.FromDto())),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Signer, ErrorResult>> DisableSignerAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new DTOs.UpdateSignerRequest
        {
            Data = new DTOs.UpdateSignerRequestData
            {
                State = DTOs.SignerState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<DTOs.SignerResponse, Signer>(
            method: HttpMethod.Patch,
            endpoint: $"{SignersEndpoint}/{signerId}",
            successFunc: r => Try.Success<Signer, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<ClientDevice, ErrorResult>> CreateClientAsync(AccessToken token, Guid? clientId = null, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(string.Empty);

        return await ProcessRequestAsync<DTOs.ClientResponse, ClientDevice>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId ?? Guid.NewGuid()}",
            successFunc: r => Try.Success<ClientDevice, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<ClientDevice, ErrorResult>> GetClientByIdAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.ClientResponse, ClientDevice>(
            method: HttpMethod.Get,
            endpoint: $"{ClientsEndpoint}/{clientId}",
            request: null,
            successFunc: r => Try.Success<ClientDevice, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<IEnumerable<ClientDevice>, ErrorResult>> GetAllClientsAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.MultipleClientResponse, IEnumerable<ClientDevice>>(
            method: HttpMethod.Get,
            endpoint: ClientsEndpoint,
            request: null,
            successFunc: r => Try.Success<IEnumerable<ClientDevice>, ErrorResult>(r.Results.Select(v => v.FromDto())),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<ClientDevice, ErrorResult>> DisableClientAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(new DTOs.UpdateClientRequest
        {
            Data = new DTOs.UpdateClientRequestData
            {
                State = DTOs.ClientState.DISABLED
            }
        }), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<DTOs.ClientResponse, ClientDevice>(
            method: HttpMethod.Patch,
            endpoint: $"{ClientsEndpoint}/{clientId}",
            successFunc: r => Try.Success<ClientDevice, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<InvoiceResponse, ErrorResult>> SendSimplifiedInvoiceAsync(
        AccessToken token,
        SimplifiedInvoice simplifiedInvoice,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateSimplifiedInvoice(simplifiedInvoice)), Encoding.UTF8, JsonContentType);
        
        return await ProcessRequestAsync<DTOs.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => Try.Success<InvoiceResponse, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<InvoiceResponse, ErrorResult>> SendCompleteInvoiceAsync(
        AccessToken token,
        CompleteInvoice completeInvoice,
        Guid clientId,
        Guid invoiceId,
        CancellationToken cancellationToken = default)
    {
        var requestBody = new StringContent(JsonSerializer.Serialize(RequestMapper.CreateCompleteInvoice(completeInvoice)), Encoding.UTF8, JsonContentType);

        return await ProcessRequestAsync<DTOs.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"{ClientsEndpoint}/{clientId}/invoices/{invoiceId}",
            successFunc: r => Try.Success<InvoiceResponse, ErrorResult>(r.FromDto()),
            token: token,
            request: requestBody,
            cancellationToken: cancellationToken
        );
    }
    
    public async Task<Try<SoftwareAuditData, ErrorResult>> GetSoftwareAuditDataAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await ProcessRequestAsync<DTOs.SoftwareResponse, SoftwareAuditData>(
            method: HttpMethod.Get,
            endpoint: SoftwareEndpoint,
            request: null,
            successFunc: r => Try.Success<SoftwareAuditData, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }
    
    private async Task<Try<TResult, ErrorResult>> ProcessRequestAsync<TDto, TResult>(
        HttpMethod method,
        string endpoint,
        StringContent request,
        Func<TDto, Try<TResult, ErrorResult>> successFunc,
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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(AuthSchema, token.Value);
        }
        var httpResponse = await _httpClient.SendAsync(requestMessage, cancellationToken);
        
        return await DeserializeAsync(httpResponse, successFunc, cancellationToken);
    }

    private async Task<Try<TResult, ErrorResult>> DeserializeAsync<TDto, TResult>(HttpResponseMessage response, Func<TDto, Try<TResult, ErrorResult>> successFunc, CancellationToken cancellationToken)
        where TDto : class
        where TResult : class
    {
        var content = await response.Content.ReadAsStringAsync(cancellationToken);
        
        return response.IsSuccessStatusCode ?
            successFunc(JsonSerializer.Deserialize<TDto>(content)) :
            Try.Error<TResult, ErrorResult>(JsonSerializer.Deserialize<DTOs.FiskalyErrorResponse>(content).FromDto());
    }
}