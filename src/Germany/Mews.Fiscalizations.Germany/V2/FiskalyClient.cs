using Mews.Fiscalizations.Germany.V2.Model;

namespace Mews.Fiscalizations.Germany.V2;

public sealed class FiskalyClient
{
    public ApiKey ApiKey { get; }
    public ApiSecret ApiSecret { get; }

    private Client Client { get; }

    public FiskalyClient(ApiKey apiKey, ApiSecret apiSecret)
    {
        ApiKey = apiKey;
        ApiSecret = apiSecret;
        Client = new Client();
    }

    public async Task<ResponseResult<Model.Client>> GetClientAsync(AccessToken token, Guid clientId, Guid tssId, CancellationToken cancellationToken = default)
    {
        return await Client.GetResponseAsync<Dto.ClientResponse, Model.Client>(
            endpoint: $"tss/{tssId}/client/{clientId}",
            successFunc: response => new ResponseResult<Model.Client>(successResult: ModelMapper.MapClient(response)),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<IEnumerable<Model.Client>>> GetRegisteredTssClientsAsync(AccessToken token, Guid tssId, CancellationToken cancellationToken = default)
    {
        return await Client.GetResponseAsync<Dto.MultipleClientResponse, IEnumerable<Model.Client>>(
            endpoint: $"tss/{tssId}/client?state=REGISTERED",
            successFunc: response => new ResponseResult<IEnumerable<Model.Client>>(successResult: response.Clients.Select(c => ModelMapper.MapClient(c))),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Model.Client>> CreateClientAsync(AccessToken token, Guid tssId, Guid? clientId = null, CancellationToken cancellationToken = default)
    {
        var id = clientId ?? Guid.NewGuid();
        return await Client.ProcessRequestAsync<Dto.CreateClientRequest, Dto.ClientResponse, Model.Client>(
            method: HttpMethod.Put,
            endpoint: $"tss/{tssId}/client/{id}",
            request: RequestCreator.CreateClient(id.ToString()),
            successFunc: response => new ResponseResult<Model.Client>(successResult: ModelMapper.MapClient(response)),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Model.Client>> UpdateClientAsync(AccessToken token, Guid tssId, Guid clientId, ClientState state, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.UpdateClientRequest, Dto.ClientResponse, Model.Client>(
            method: new HttpMethod("PATCH"),
            endpoint: $"tss/{tssId}/client/{clientId}",
            request: RequestCreator.UpdateClient(state),
            successFunc: response => new ResponseResult<Model.Client>(successResult: ModelMapper.MapClient(response)),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Tss>> GetTssAsync(AccessToken token, Guid tssId, CancellationToken cancellationToken = default)
    {
        return await Client.GetResponseAsync<Dto.TssResponse, Tss>(
            endpoint: $"tss/{tssId}",
            successFunc: response => new ResponseResult<Tss>(ModelMapper.MapTss(response)),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    /// <summary>
    /// <para>Returns all TSSs that are not disabled or enabled.</para>
    /// <para>Disabled state is used to put the TSS out of service permanently.</para>
    /// <para>Deleted state is only available in the test environment and it is used by Fiskaly to wipe the test data every week.</para>
    /// </summary>
    public async Task<ResponseResult<IEnumerable<Tss>>> GetAllEnabledTSSsAsync(AccessToken token, CancellationToken cancellationToken = default)
    {
        return await Client.GetResponseAsync<Dto.MultipleTssResponse, IEnumerable<Tss>>(
            endpoint: $"tss?states=CREATED&states=INITIALIZED&states=UNINITIALIZED",
            successFunc: response => new ResponseResult<IEnumerable<Tss>>(successResult: response.TssList.Select(t => ModelMapper.MapTss(t))),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<CreateTssResult>> CreateTssAsync(AccessToken token, Guid? tssId = null, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<object, Dto.CreateTssResponse, CreateTssResult>(
            method: HttpMethod.Put,
            endpoint: $"tss/{tssId ?? Guid.NewGuid()}",
            successFunc: response => ModelMapper.MapCreateTss(response),
            token: token,
            request: new { },
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Tss>> UpdateTssAsync(AccessToken token, Guid tssId, TssState state, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.UpdateTssRequest, Dto.TssResponse, Tss>(
            method: new HttpMethod("PATCH"),
            endpoint: $"tss/{tssId}",
            request: RequestCreator.UpdateTss(state),
            successFunc: response => new ResponseResult<Tss>(ModelMapper.MapTss(response)),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Transaction>> GetTransactionAsync(AccessToken token, Guid tssId, Guid transactionId, CancellationToken cancellationToken = default)
    {
        return await Client.GetResponseAsync<Dto.TransactionResponse, Transaction>(
            endpoint: $"tss/{tssId}/tx/{transactionId}",
            successFunc: response => ModelMapper.MapTransaction(response),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Transaction>> StartTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Guid transactionId, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.TransactionRequest, Dto.TransactionResponse, Transaction>(
            method: HttpMethod.Put,
            endpoint: $"tss/{tssId}/tx/{transactionId}?tx_revision=1",
            request: RequestCreator.CreateTransaction(clientId),
            successFunc: response => ModelMapper.MapTransaction(response),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Transaction>> FinishTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Bill bill, Guid transactionId, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.FinishTransactionRequest, Dto.TransactionResponse, Transaction>(
            method: HttpMethod.Put,
            endpoint: $"tss/{tssId}/tx/{transactionId}?tx_revision=2",
            request: RequestCreator.FinishTransaction(clientId, bill),
            successFunc: response => ModelMapper.MapTransaction(response),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<AccessToken>> GetAccessTokenAsync(CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.AuthorizationTokenRequest, Dto.AuthorizationTokenResponse, AccessToken>(
            method: HttpMethod.Post,
            endpoint: "auth",
            request: RequestCreator.CreateAuthorizationToken(ApiKey, ApiSecret),
            successFunc: response => ModelMapper.MapAccessToken(response),
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Nothing>> AdminLoginAsync(AccessToken token, Guid tssId, string adminPin, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.AdminLoginRequest, object, Nothing>(
            method: HttpMethod.Post,
            endpoint: $"tss/{tssId}/admin/auth",
            request: RequestCreator.AdminLoginRequest(adminPin),
            successFunc: _ => new ResponseResult<Nothing>(),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<string>> ChangeAdminPinAsync(AccessToken token, Guid tssId, string adminPuk, string newAdminPin, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<Dto.AdminSetPinRequest, object, string>(
            method: HttpMethod.Patch,
            endpoint: $"tss/{tssId}/admin",
            request: RequestCreator.CreateAdminSetPinRequest(adminPuk, newAdminPin),
            successFunc: _ => new ResponseResult<string>(newAdminPin),
            token: token,
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<Nothing>> AdminLogoutAsync(Guid tssId, CancellationToken cancellationToken = default)
    {
        return await Client.ProcessRequestAsync<object, object, Nothing>(
            method: HttpMethod.Post,
            endpoint: $"tss/{tssId}/admin/logout",
            request: new { },
            successFunc: _ => new ResponseResult<Nothing>(),
            cancellationToken: cancellationToken
        );
    }
}