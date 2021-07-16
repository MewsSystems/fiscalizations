using Mews.Fiscalizations.Germany.Model;
using System;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
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
            var tss = await GetTssAsync(token, tssId).ConfigureAwait(continueOnCapturedContext: false);
            var tssCertificate = tss.SuccessResult.Certificate;
            var certificate = new X509Certificate2(Encoding.UTF8.GetBytes(tssCertificate));
            var serialNumber = certificate.GetCertHashString();

            var request = RequestCreator.CreateClient(serialNumber);
            return await Client.ProcessRequestAsync<Dto.CreateClientRequest, Dto.ClientResponse, Model.Client>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/client/{Guid.NewGuid()}",
                request: request,
                successFunc: response => ModelMapper.MapClient(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public Task<ResponseResult<Tss>> GetTssAsync(AccessToken token, Guid tssId)
        {
            return Client.GetResponseAsync<Dto.TssResponse, Tss>(
                endpoint: $"tss/{tssId}",
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            );
        }

        public Task<ResponseResult<Tss>> CreateTssAsync(AccessToken token, TssState state, string description = null)
        {
            return Client.ProcessRequestAsync<Dto.CreateTssRequest, Dto.TssResponse, Tss>(
                method: HttpMethod.Put,
                endpoint: $"tss/{Guid.NewGuid()}",
                request: RequestCreator.CreateTss(state, description),
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            );
        }

        public Task<ResponseResult<Tss>> UpdateTssAsync(AccessToken token, Guid tssId, TssState state)
        {
            return Client.ProcessRequestAsync<Dto.UpdateTssRequest, Dto.TssResponse, Tss>(
                method: HttpMethod.Put,
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
                endpoint: $"tss/{tssId}/tx/{transactionId}",
                request: RequestCreator.CreateTransaction(clientId),
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            );
        }

        public Task<ResponseResult<Transaction>> FinishTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Bill bill, Guid transactionId, string lastRevision)
        {
            return Client.ProcessRequestAsync<Dto.FinishTransactionRequest, Dto.TransactionResponse, Transaction>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/tx/{transactionId}?last_revision={lastRevision}",
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
    }
}
