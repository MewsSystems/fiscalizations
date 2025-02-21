namespace Mews.Fiscalizations.Hungary;

public sealed class NavClient
{
    public NavClient(HttpClient httpClient, TechnicalUser technicalUser, SoftwareIdentification softwareIdentification, NavEnvironment environment)
    {
        TechnicalUser = technicalUser;
        SoftwareIdentification = softwareIdentification;
        Client = new Client(httpClient, environment);
    }

    private TechnicalUser TechnicalUser { get; }

    private SoftwareIdentification SoftwareIdentification { get; }

    private Client Client { get; }

    public async Task<ResponseResult<ExchangeToken, ExchangeTokenErrorCode>> GetExchangeTokenAsync(CancellationToken cancellationToken = default)
    {
        var request = RequestCreator.CreateTokenExchangeRequest(TechnicalUser, SoftwareIdentification);
        return await Client.ProcessRequestAsync<Dto.TokenExchangeRequest, Dto.TokenExchangeResponse, ExchangeToken, ExchangeTokenErrorCode>(
            endpoint: "tokenExchange",
            request: request,
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapExchangeToken(requestXml, responseXml, responseDto, TechnicalUser),
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<TransactionStatus, TransactionErrorCode>> GetTransactionStatusAsync(string transactionId, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(transactionId))
        {
            throw new ArgumentException($"{nameof(transactionId)} must be specified.");
        }

        var request = RequestCreator.CreateQueryTransactionStatusRequest(TechnicalUser, SoftwareIdentification, transactionId);
        return await Client.ProcessRequestAsync<Dto.QueryTransactionStatusRequest, Dto.QueryTransactionStatusResponse, TransactionStatus, TransactionErrorCode>(
            endpoint: "queryTransactionStatus",
            request: request,
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapTransactionStatus(requestXml, responseXml, responseDto),
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<TaxPayerData, TaxPayerErrorCode>> GetTaxPayerDataAsync(TaxpayerIdentificationNumber taxId, CancellationToken cancellationToken = default)
    {
        var request = RequestCreator.CreateQueryTaxpayerRequest(TechnicalUser, SoftwareIdentification, taxId.TaxpayerNumber);
        return await Client.ProcessRequestAsync<Dto.QueryTaxpayerRequest, Dto.QueryTaxpayerResponse, TaxPayerData, TaxPayerErrorCode>(
            endpoint: "queryTaxpayer",
            request: request,
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapTaxPayerData(requestXml, responseXml, responseDto),
            cancellationToken: cancellationToken
        );
    }

    public async Task<ResponseResult<string, ResultErrorCode>> SendInvoicesAsync(ExchangeToken token, ISequence<Invoice> invoices, CancellationToken cancellationToken = default)
    {
        var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
        return await ManageInvoicesAsync(request, invoices, cancellationToken);
    }

    public async Task<ResponseResult<string, ResultErrorCode>> SendModificationDocumentsAsync(ExchangeToken token, ISequence<ModificationInvoice> invoices, CancellationToken cancellationToken = default)
    {
        var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
        return await ManageInvoicesAsync(request, invoices, cancellationToken);
    }

    private async Task<ResponseResult<string, ResultErrorCode>> ManageInvoicesAsync<TDocument>(Dto.ManageInvoiceRequest request, ISequence<TDocument> invoices, CancellationToken cancellationToken = default)
    {
        if (invoices.Values.Count > ServiceInfo.MaxInvoiceBatchSize)
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
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapManageInvoice(requestXml, responseXml, responseDto),
            cancellationToken: cancellationToken
        );
    }
}