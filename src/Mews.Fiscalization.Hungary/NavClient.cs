using Mews.Fiscalization.Core.Model;
using Mews.Fiscalization.Hungary.Models;
using Mews.Fiscalization.Hungary.Utils;
using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mews.Fiscalization.Hungary
{
    public sealed class NavClient
    {
        private TechnicalUser TechnicalUser { get; }

        private SoftwareIdentification SoftwareIdentification { get; }

        private static HttpClient HttpClient { get; }

        private Client Client { get; }

        static NavClient()
        {
            HttpClient = new HttpClient();
        }

        public NavClient(TechnicalUser technicalUser, SoftwareIdentification softwareIdentification, NavEnvironment environment)
        {
            TechnicalUser = technicalUser;
            SoftwareIdentification = softwareIdentification;
            Client = new Client(HttpClient, environment);
        }

        public async Task<ResponseResult<ExchangeToken, ExchangeTokenErrorCode>> GetExchangeTokenAsync()
        {
            var request = RequestCreator.CreateTokenExchangeRequest(TechnicalUser, SoftwareIdentification);
            return await Client.ProcessRequestAsync<Dto.TokenExchangeRequest, Dto.TokenExchangeResponse, ExchangeToken, ExchangeTokenErrorCode>(
                endpoint: "tokenExchange",
                request: request,
                successFunc: response => ModelMapper.MapExchangeToken(response, TechnicalUser)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<TransactionStatus, TransactionErrorCode>> GetTransactionStatusAsync(string transactionId)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentException($"{nameof(transactionId)} must be specified.");
            }

            var request = RequestCreator.CreateQueryTransactionStatusRequest(TechnicalUser, SoftwareIdentification, transactionId);
            return await Client.ProcessRequestAsync<Dto.QueryTransactionStatusRequest, Dto.QueryTransactionStatusResponse, TransactionStatus, TransactionErrorCode>(
                endpoint: "queryTransactionStatus",
                request: request,
                successFunc: response => ModelMapper.MapTransactionStatus(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<TaxPayerData, TaxPayerErrorCode>> GetTaxPayerDataAsync(TaxpayerIdentificationNumber taxId)
        {
            var request = RequestCreator.CreateQueryTaxpayerRequest(TechnicalUser, SoftwareIdentification, taxId.TaxpayerNumber);
            return await Client.ProcessRequestAsync<Dto.QueryTaxpayerRequest, Dto.QueryTaxpayerResponse, TaxPayerData, TaxPayerErrorCode>(
                endpoint: "queryTaxpayer",
                request: request,
                successFunc: response => ModelMapper.MapTaxPayerData(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }

        public async Task<ResponseResult<string, ResultErrorCode>> SendInvoicesAsync(ExchangeToken token, ISequence<Invoice> invoices)
        {
            var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
            return await ManageInvoicesAsync(request, invoices);
        }

        public async Task<ResponseResult<string, ResultErrorCode>> SendModificationDocumentsAsync(ExchangeToken token, ISequence<ModificationInvoice> invoices)
        {
            var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
            return await ManageInvoicesAsync(request, invoices);
        }

        private async Task<ResponseResult<string, ResultErrorCode>> ManageInvoicesAsync<TDocument>(Dto.ManageInvoiceRequest request, ISequence<TDocument> invoices)
        {
            if (invoices.Values.Count() > ServiceInfo.MaxInvoiceBatchSize)
            {
                throw new ArgumentException($"Max invoice batch size ({ServiceInfo.MaxInvoiceBatchSize}) exceeded.", nameof(invoices));
            }

            if (invoices.StartIndex != 1)
            {
                throw new ArgumentException("Items need to be indexed from 1.", nameof(invoices));
            }

            return await Client.ProcessRequestAsync<Dto.ManageInvoiceRequest, Dto.ManageInvoiceResponse, string, ResultErrorCode>(
                endpoint: "manageInvoice",
                request: request,
                successFunc: response => ModelMapper.MapManageInvoice(response)
            ).ConfigureAwait(continueOnCapturedContext: false);
        }
    }
}