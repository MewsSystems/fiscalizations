using FuncSharp;
using Mews.Fiscalizations.Germany.Model;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany
{
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

        public Task<ResponseResult<Model.Client>> GetClientAsync(AccessToken token, Guid clientId, Guid tssId)
        {
            return Client.GetResponseAsync<Dto.ClientResponse, Model.Client>(
                endpoint: $"tss/{tssId}/client/{clientId}",
                successFunc: response => ModelMapper.MapClient(response),
                token: token
            );
        }

        public async Task<ResponseResult<Model.Client>> CreateClientAsync(AccessToken token, Guid tssId)
        {
            var id = Guid.NewGuid();
            return await Client.ProcessRequestAsync<Dto.CreateClientRequest, Dto.ClientResponse, Model.Client>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/client/{id}",
                request: RequestCreator.CreateClient(id.ToString()),
                successFunc: response => ModelMapper.MapClient(response),
                token: token
            );
        }

        public Task<ResponseResult<Model.Client>> UpdateClientAsync(AccessToken token, Guid tssId, Guid clientId, ClientState state)
        {
            return Client.ProcessRequestAsync<Dto.UpdateClientRequest, Dto.ClientResponse, Model.Client>(
                method: new HttpMethod("PATCH"),
                endpoint: $"tss/{tssId}/client/{clientId}",
                request: RequestCreator.UpdateClient(state),
                successFunc: response => ModelMapper.MapClient(response),
                token: token
            );
        }

        public Task<ResponseResult<Tss>> GetTssAsync(AccessToken token, Guid tssId)
        {
            return Client.GetResponseAsync<Dto.TssResponse, Tss>(
                endpoint: $"tss/{tssId}",
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            );
        }

        public Task<ResponseResult<CreateTssResult>> CreateTssAsync(AccessToken token)
        {
            return Client.ProcessRequestAsync<object, Dto.CreateTssResponse, CreateTssResult>(
                method: HttpMethod.Put,
                endpoint: $"tss/{Guid.NewGuid()}",
                successFunc: response => ModelMapper.MapCreateTss(response),
                token: token,
                request: new { }
            );
        }

        public async Task<ResponseResult<Tss>> UpdateTssAsync(AccessToken token, Guid tssId, TssState state)
        {
            return await Client.ProcessRequestAsync<Dto.UpdateTssRequest, Dto.TssResponse, Tss>(
                method: new HttpMethod("PATCH"),
                endpoint: $"tss/{tssId}",
                request: RequestCreator.UpdateTss(state),
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            );
        }

        public Task<ResponseResult<Transaction>> GetTransactionAsync(AccessToken token, Guid tssId, Guid transactionId)
        {
            return Client.GetResponseAsync<Dto.TransactionResponse, Transaction>(
                endpoint: $"tss/{tssId}/tx/{transactionId}",
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            );
        }

        public Task<ResponseResult<Transaction>> StartTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Guid transactionId)
        {
            return Client.ProcessRequestAsync<Dto.TransactionRequest, Dto.TransactionResponse, Transaction>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/tx/{transactionId}?tx_revision=1",
                request: RequestCreator.CreateTransaction(clientId),
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            );
        }

        public Task<ResponseResult<Transaction>> FinishTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Bill bill, Guid transactionId, int revision = 2)
        {
            return Client.ProcessRequestAsync<Dto.FinishTransactionRequest, Dto.TransactionResponse, Transaction>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/tx/{transactionId}?tx_revision={revision}",
                request: RequestCreator.FinishTransaction(clientId, bill),
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            );
        }

        public Task<ResponseResult<AccessToken>> GetAccessTokenAsync()
        {
            return Client.ProcessRequestAsync<Dto.AuthorizationTokenRequest, Dto.AuthorizationTokenResponse, AccessToken>(
                method: HttpMethod.Post,
                endpoint: "auth",
                request: RequestCreator.CreateAuthorizationToken(ApiKey, ApiSecret),
                successFunc: response => ModelMapper.MapAccessToken(response)
            );
        }

        public Task<ResponseResult<Nothing>> AdminLoginAsync(AccessToken token, Guid tssId, string adminPin)
        {
            return Client.ProcessRequestAsync<Dto.AdminLoginRequest, object, Nothing>(
                method: HttpMethod.Post,
                endpoint: $"tss/{tssId}/admin/auth",
                request: RequestCreator.AdminLoginRequest(adminPin),
                successFunc: response => new ResponseResult<Nothing>(),
                token: token
            );
        }

        public Task<ResponseResult<string>> ChangeAdminPinAsync(AccessToken token, Guid tssId, string adminPuk, string newAdminPin)
        {
            return Client.ProcessRequestAsync<Dto.AdminSetPinRequest, object, string>(
                method: new HttpMethod("PATCH"), // TODO: Update .net standard and then use HttpMethod.Patch.
                endpoint: $"tss/{tssId}/admin",
                request: RequestCreator.CreateAdminSetPinRequest(adminPuk, newAdminPin),
                successFunc: response => new ResponseResult<string>(newAdminPin),
                token: token
            );
        }

        public Task<ResponseResult<Nothing>> AdminLogoutAsync(Guid tssId)
        {
            return Client.ProcessRequestAsync<object, object, Nothing>(
                method: HttpMethod.Post,
                endpoint: $"tss/{tssId}/admin/logout",
                request: new { },
                successFunc: response => new ResponseResult<Nothing>()
            );
        }
    }
}
