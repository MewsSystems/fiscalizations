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

    /// <summary>
    /// Issues a data reporting token. NAV tokens are single use, so one is taken per manageInvoice call and
    /// never reused - including when a call is repeated.
    /// </summary>
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

    /// <param name="returnOriginalRequest">
    /// Asks NAV to return the data that was submitted, which is how a transaction recovered from
    /// /queryTransactionList is matched back to the invoice it carried.
    /// </param>
    public async Task<ResponseResult<TransactionStatus, TransactionErrorCode>> GetTransactionStatusAsync(string transactionId, bool returnOriginalRequest = false, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(transactionId))
        {
            throw new ArgumentException($"{nameof(transactionId)} must be specified.");
        }

        var request = RequestCreator.CreateQueryTransactionStatusRequest(TechnicalUser, SoftwareIdentification, transactionId, returnOriginalRequest);
        return await Client.ProcessRequestAsync<Dto.QueryTransactionStatusRequest, Dto.QueryTransactionStatusResponse, TransactionStatus, TransactionErrorCode>(
            endpoint: "queryTransactionStatus",
            request: request,
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapTransactionStatus(requestXml, responseXml, responseDto),
            cancellationToken: cancellationToken
        );
    }

    /// <summary>
    /// Lists the transactions NAV received in a period. Used to recover a transaction id that was lost to a
    /// client timeout, so an invoice is not reported twice (spec 1.9.2).
    /// </summary>
    public async Task<ResponseResult<TransactionList, TransactionErrorCode>> GetTransactionListAsync(int page, DateTime insertedFromUtc, DateTime insertedToUtc, CancellationToken cancellationToken = default)
    {
        var request = RequestCreator.CreateQueryTransactionListRequest(TechnicalUser, SoftwareIdentification, page, insertedFromUtc, insertedToUtc);
        return await Client.ProcessRequestAsync<Dto.QueryTransactionListRequest, Dto.QueryTransactionListResponse, TransactionList, TransactionErrorCode>(
            endpoint: "queryTransactionList",
            request: request,
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapTransactionList(requestXml, responseXml, responseDto),
            cancellationToken: cancellationToken
        );
    }

    /// <summary>
    /// Returns what NAV already holds for an invoice number: the original report and every modification.
    /// A modification document's modificationIndex and line numbering continue from this.
    /// </summary>
    public async Task<ResponseResult<InvoiceChain, TransactionErrorCode>> GetInvoiceChainDigestAsync(string invoiceNumber, int page = 1, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrEmpty(invoiceNumber))
        {
            throw new ArgumentException($"{nameof(invoiceNumber)} must be specified.");
        }

        var request = RequestCreator.CreateQueryInvoiceChainDigestRequest(TechnicalUser, SoftwareIdentification, page, invoiceNumber);
        return await Client.ProcessRequestAsync<Dto.QueryInvoiceChainDigestRequest, Dto.QueryInvoiceChainDigestResponse, InvoiceChain, TransactionErrorCode>(
            endpoint: "queryInvoiceChainDigest",
            request: request,
            successFunc: (responseDto, requestXml, responseXml) => ModelMapper.MapInvoiceChain(requestXml, responseXml, responseDto),
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
        CheckOperation(invoices, ModificationOperation.Modify);
        var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
        return await ManageInvoicesAsync(request, invoices, cancellationToken);
    }

    /// <summary>
    /// Reports documents that cancel their original invoice in full, which NAV calls STORNO. A document that
    /// changes an invoice rather than withdrawing it goes through
    /// <see cref="SendModificationDocumentsAsync"/> instead.
    /// </summary>
    public async Task<ResponseResult<string, ResultErrorCode>> SendCancellationDocumentsAsync(ExchangeToken token, ISequence<ModificationInvoice> invoices, CancellationToken cancellationToken = default)
    {
        CheckOperation(invoices, ModificationOperation.Storno);
        var request = RequestCreator.CreateManageInvoicesRequest(TechnicalUser, SoftwareIdentification, token, invoices);
        return await ManageInvoicesAsync(request, invoices, cancellationToken);
    }

    /// <summary>
    /// NAV takes one invoiceOperation per request, so a sequence mixing MODIFY and STORNO would silently
    /// report all of them under whichever operation came first.
    /// </summary>
    private static void CheckOperation(ISequence<ModificationInvoice> invoices, ModificationOperation expected)
    {
        if (invoices.Values.Any(i => i.Value.Operation != expected))
        {
            throw new ArgumentException($"Every document in the request must have the {expected} operation.", nameof(invoices));
        }
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
