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

        public async Task<ResponseResult<Model.Client>> GetClientAsync(AccessToken token, Guid clientId, Guid tssId)
        {
            return await Client.ProcessRequestAsync<Dto.ClientRequest, Dto.ClientResponse, Model.Client>(
                method: HttpMethod.Get,
                endpoint: $"tss/{tssId}/client/{clientId}",
                request: null,
                successFunc: response => ModelMapper.MapClient(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
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

        public async Task<ResponseResult<Tss>> GetTssAsync(AccessToken token, Guid tssId)
        {
            return await Client.ProcessRequestAsync<Dto.TssRequest, Dto.TssResponse, Tss>(
                method: HttpMethod.Get,
                endpoint: $"tss/{tssId}",
                request: null,
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<Tss>> CreateTssAsync(AccessToken token, TssState state, string description = null)
        {
            var request = RequestCreator.CreateTss(state, description);
            return await Client.ProcessRequestAsync<Dto.CreateTssRequest, Dto.TssResponse, Tss>(
                method: HttpMethod.Put,
                endpoint: $"tss/{Guid.NewGuid()}",
                request: request,
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<Tss>> UpdateTssAsync(AccessToken token, Guid tssId, TssState state)
        {
            var request = RequestCreator.UpdateTss(state);
            return await Client.ProcessRequestAsync<Dto.UpdateTssRequest, Dto.TssResponse, Tss>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}",
                request: request,
                successFunc: response => ModelMapper.MapTss(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<Transaction>> GetTransactionAsync(AccessToken token, Guid tssId, Guid transactionId)
        {
            var request = RequestCreator.GetTransaction(tssId, transactionId);
            return await Client.ProcessRequestAsync<Dto.GetTransactionRequest, Dto.TransactionResponse, Transaction>(
                method: HttpMethod.Get,
                endpoint: $"tss/{tssId}/tx/{transactionId}",
                request: request,
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<Transaction>> StartTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Guid transactionId)
        {
            var request = RequestCreator.CreateTransaction(clientId);
            return await Client.ProcessRequestAsync<Dto.TransactionRequest, Dto.TransactionResponse, Transaction>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/tx/{transactionId}",
                request: request,
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<Transaction>> FinishTransactionAsync(AccessToken token, Guid clientId, Guid tssId, Bill bill, Guid transactionId, string lastRevision)
        {
            var request = RequestCreator.FinishTransaction(clientId, bill);
            return await Client.ProcessRequestAsync<Dto.FinishTransactionRequest, Dto.TransactionResponse, Transaction>(
                method: HttpMethod.Put,
                endpoint: $"tss/{tssId}/tx/{transactionId}?last_revision={lastRevision}",
                request: request,
                successFunc: response => ModelMapper.MapTransaction(response),
                token: token
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<AccessToken>> GetAccessTokenAsync()
        {
            var request = RequestCreator.CreateAuthorizationToken(ApiKey, ApiSecret);
            return await Client.ProcessRequestAsync<Dto.AuthorizationTokenRequest, Dto.AuthorizationTokenResponse, AccessToken>(
                method: HttpMethod.Post,
                endpoint: "auth",
                request: request,
                successFunc: response => ModelMapper.MapAccessToken(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}
