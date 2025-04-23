using Mews.Fiscalizations.Verifactu.Models;

namespace Mews.Fiscalizations.Verifactu;

public class FiskalyClient
{
    public FiskalyClient(HttpClient httpClient, FiskalyEnvironment environment, string apiKey, string apiSecret)
    {
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        InternalClient = new InternalClient(httpClient, environment);
    }

    private string ApiKey { get; }

    private string ApiSecret { get; }

    private InternalClient InternalClient { get; }

    public async Task<Try<AccessToken, ErrorResult>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        return await InternalClient.ProcessRequestAsync<DTOs.AuthorizationTokenRequest, DTOs.AuthorizationTokenResponse, AccessToken>(
            method: HttpMethod.Post,
            endpoint: "auth",
            request: RequestMapper.CreateAuthorizationToken(ApiKey, ApiSecret),
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
        return await InternalClient.ProcessRequestAsync<DTOs.CreateTaxpayerRequest, DTOs.TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Put,
            endpoint: "taxpayer",
            request: RequestMapper.CreateTaxpayer(legalName, taxIdentifier, territory),
            successFunc: r => Try.Success<Taxpayer, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Taxpayer, ErrorResult>> GetTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await InternalClient.GetResponseAsync<DTOs.TaxpayerResponse, Taxpayer>(
            endpoint: "taxpayer",
            successFunc: r => Try.Success<Taxpayer, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Taxpayer, ErrorResult>> DisableTaxpayerAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await InternalClient.ProcessRequestAsync<DTOs.UpdateTaxpayerRequest, DTOs.TaxpayerResponse, Taxpayer>(
            method: HttpMethod.Patch,
            endpoint: "taxpayer",
            successFunc: r => Try.Success<Taxpayer, ErrorResult>(r.FromDto()),
            token: token,
            request: new DTOs.UpdateTaxpayerRequest
            {
                Data = new DTOs.UpdateTaxpayerRequestData
                {
                    State = DTOs.TaxpayerState.DISABLED
                }
            },
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Signer, ErrorResult>> CreateSignerAsync(AccessToken token, Guid? signerId = null, CancellationToken cancellationToken = default)
    {
        return await InternalClient.ProcessRequestAsync<object, DTOs.SignerResponse, Signer>(
            method: HttpMethod.Put,
            endpoint: $"signers/{signerId ?? Guid.NewGuid()}",
            successFunc: r => Try.Success<Signer, ErrorResult>(r.FromDto()),
            token: token,
            request: new { },
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Signer, ErrorResult>> GetSignerByIdAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        return await InternalClient.GetResponseAsync<DTOs.SignerResponse, Signer>(
            endpoint: $"signers/{signerId}",
            successFunc: r => Try.Success<Signer, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<IEnumerable<Signer>, ErrorResult>> GetAllSignersAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await InternalClient.GetResponseAsync<DTOs.MultipleSignerResponse, IEnumerable<Signer>>(
            endpoint: "signers",
            successFunc: r => Try.Success<IEnumerable<Signer>, ErrorResult>(r.Results.Select(v => v.FromDto())),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<Signer, ErrorResult>> DisableSignerAsync(AccessToken token, Guid signerId, CancellationToken cancellationToken = default)
    {
        return await InternalClient.ProcessRequestAsync<DTOs.UpdateSignerRequest, DTOs.SignerResponse, Signer>(
            method: HttpMethod.Patch,
            endpoint: $"signers/{signerId}",
            successFunc: r => Try.Success<Signer, ErrorResult>(r.FromDto()),
            token: token,
            request: new DTOs.UpdateSignerRequest
            {
                Data = new DTOs.UpdateSignerRequestData
                {
                    State = DTOs.SignerState.DISABLED
                }
            },
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<ClientDevice, ErrorResult>> CreateClientAsync(AccessToken token, Guid? clientId = null, CancellationToken cancellationToken = default)
    {
        return await InternalClient.ProcessRequestAsync<object, DTOs.ClientResponse, ClientDevice>(
            method: HttpMethod.Put,
            endpoint: $"clients/{clientId ?? Guid.NewGuid()}",
            successFunc: r => Try.Success<ClientDevice, ErrorResult>(r.FromDto()),
            token: token,
            request: new { },
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<ClientDevice, ErrorResult>> GetClientByIdAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        return await InternalClient.GetResponseAsync<DTOs.ClientResponse, ClientDevice>(
            endpoint: $"clients/{clientId}",
            successFunc: r => Try.Success<ClientDevice, ErrorResult>(r.FromDto()),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<IEnumerable<ClientDevice>, ErrorResult>> GetAllClientsAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await InternalClient.GetResponseAsync<DTOs.MultipleClientResponse, IEnumerable<ClientDevice>>(
            endpoint: "clients",
            successFunc: r => Try.Success<IEnumerable<ClientDevice>, ErrorResult>(r.Results.Select(v => v.FromDto())),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<ClientDevice, ErrorResult>> DisableClientAsync(AccessToken token, Guid clientId, CancellationToken cancellationToken = default)
    {
        return await InternalClient.ProcessRequestAsync<DTOs.UpdateClientRequest, DTOs.ClientResponse, ClientDevice>(
            method: HttpMethod.Patch,
            endpoint: $"clients/{clientId}",
            successFunc: r => Try.Success<ClientDevice, ErrorResult>(r.FromDto()),
            token: token,
            request: new DTOs.UpdateClientRequest
            {
                Data = new DTOs.UpdateClientRequestData
                {
                    State = DTOs.ClientState.DISABLED
                }
            },
            cancellationToken: cancellationToken
        );
    }

    public async Task<Try<SoftwareAuditData, ErrorResult>> GetSoftwareAuditDataAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await InternalClient.GetResponseAsync<DTOs.SoftwareResponse, SoftwareAuditData>(
            endpoint: "software",
            successFunc: r => Try.Success<SoftwareAuditData, ErrorResult>(r.FromDto()),
            token: token,
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
        return await InternalClient.ProcessRequestAsync<DTOs.SimplifiedInvoiceRequest, DTOs.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"clients/{clientId}/invoices/{invoiceId}",
            successFunc: r => Try.Success<InvoiceResponse, ErrorResult>(r.FromDto()),
            token: token,
            request: RequestMapper.CreateSimplifiedInvoice(simplifiedInvoice),
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
        return await InternalClient.ProcessRequestAsync<DTOs.CompleteInvoiceRequest, DTOs.InvoiceResponse, InvoiceResponse>(
            method: HttpMethod.Put,
            endpoint: $"clients/{clientId}/invoices/{invoiceId}",
            successFunc: r => Try.Success<InvoiceResponse, ErrorResult>(r.FromDto()),
            token: token,
            request: RequestMapper.CreateCompleteInvoice(completeInvoice),
            cancellationToken: cancellationToken
        );
    }
}