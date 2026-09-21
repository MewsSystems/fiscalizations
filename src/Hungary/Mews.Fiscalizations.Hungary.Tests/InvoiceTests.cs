namespace Mews.Fiscalizations.Hungary.Tests;

/// <summary>
/// Exercises the live NAV test environment, so it needs the hungarian_* environment variables from
/// <see cref="TestFixture"/>. The offline coverage of the same mapping is in
/// <see cref="RequestMapperTests"/> and <see cref="SchemaValidationTests"/>.
/// </summary>
[TestFixture]
public sealed class InvoiceTests
{
    private static readonly NavClient NavClient = TestFixture.GetNavClient();

    private const int RetryCount = 3;

    [Test]
    [Retry(RetryCount)]
    public async Task SendCustomerInvoiceSucceeds()
    {
        var receiver = Receiver.Customer();
        var sendInvoicesResult = await SendInvoices(receiver);
        await AssertInvoices(sendInvoicesResult);
    }

    [Test]
    [Retry(RetryCount)]
    public async Task SendLocalCompanyInvoiceSucceeds()
    {
        var sendInvoicesResult = await SendInvoices(LocalCompanyReceiver());
        await AssertInvoices(sendInvoicesResult);
    }

    [Test]
    [Retry(RetryCount)]
    [TestCase("CZ", "CZ12345678")]
    [TestCase("US", "UsTaxId")]
    [TestCase("CZ", null)]
    [TestCase("US", null)]
    public async Task SendForeignCompanyInvoiceSucceeds(string countryCode, string taxId)
    {
        var sendInvoicesResult = await SendInvoices(ForeignCompanyReceiver(countryCode, taxId));
        await AssertInvoices(sendInvoicesResult);
    }

    [Test]
    [Retry(RetryCount)]
    public async Task SendAggregateInvoiceSucceeds()
    {
        // An aggregate invoice is the shape a multi-day folio takes: several supplies, each with its own
        // delivery date. NAV then runs its aggregate-only reconciliation checks over the lines.
        var sendInvoicesResult = await SendInvoices(Receiver.Customer(), category: InvoiceCategory.Aggregate);
        await AssertInvoices(sendInvoicesResult);
    }

    [Test, Order(1)]
    [Retry(RetryCount)]
    public async Task SendCorrectionCustomerInvoiceSucceeds()
    {
        var receiver = Receiver.Customer();
        var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
        var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
        await AssertInvoices(sendInvoicesResult);

        var sendModificationInvoicesResult = await SendModificationInvoices(receiver, invoiceNumber, ModificationOperation.Modify);
        await AssertInvoices(sendModificationInvoicesResult);
    }

    [Test, Order(1)]
    [Retry(RetryCount)]
    public async Task SendCancellationCustomerInvoiceSucceeds()
    {
        // A document that reverses the original invoice in full is a STORNO, not a MODIFY.
        var receiver = Receiver.Customer();
        var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
        var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
        await AssertInvoices(sendInvoicesResult);

        var sendCancellationResult = await SendModificationInvoices(receiver, invoiceNumber, ModificationOperation.Storno);
        await AssertInvoices(sendCancellationResult);
    }

    [Test, Order(1)]
    [Retry(RetryCount)]
    public async Task SendCorrectionLocalCompanyInvoiceSucceeds()
    {
        var receiver = LocalCompanyReceiver();
        var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
        var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
        await AssertInvoices(sendInvoicesResult);

        var sendModificationInvoiceResponse = await SendModificationInvoices(receiver, invoiceNumber, ModificationOperation.Modify);
        await AssertInvoices(sendModificationInvoiceResponse);
    }

    [Test, Order(1)]
    [Retry(RetryCount)]
    [TestCase("CZ", "CZ12345678")]
    [TestCase("US", "UsTaxId")]
    [TestCase("CZ", null)]
    [TestCase("US", null)]
    public async Task SendCorrectionForeignCompanyInvoiceSucceeds(string countryCode, string taxId)
    {
        var receiver = ForeignCompanyReceiver(countryCode, taxId);
        var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
        var sendInvoicesResult = await SendInvoices(receiver, invoiceNumber);
        await AssertInvoices(sendInvoicesResult);

        var sendModificationInvoiceResponse = await SendModificationInvoices(receiver, invoiceNumber, ModificationOperation.Modify);
        await AssertInvoices(sendModificationInvoiceResponse);
    }

    [Test, Order(2)]
    [Retry(RetryCount)]
    public async Task QueryInvoiceChainDigestReturnsTheReportedChain()
    {
        // The chain digest is what a modification document's modificationIndex and line numbering are read
        // from, so it has to come back describing the invoice that was just reported.
        var invoiceNumber = InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get();
        await AssertInvoices(await SendInvoices(Receiver.Customer(), invoiceNumber));

        var chain = await NavClient.GetInvoiceChainDigestAsync(invoiceNumber.Value);
        TestFixture.AssertResponse(chain);

        Assert.That(chain.SuccessResult.IsEmpty, Is.False);
        Assert.That(chain.SuccessResult.NextModificationIndex, Is.EqualTo(1));
        Assert.That(chain.SuccessResult.MaxLineNumber, Is.EqualTo(3));
    }

    [Test, Order(2)]
    [Retry(RetryCount)]
    public async Task QueryTransactionListReturnsRecentlySubmittedTransactions()
    {
        // This is the lost-transaction recovery path from spec 1.9.2: after a timeout the transaction is
        // found here rather than the invoice being submitted a second time.
        var sendInvoicesResult = await SendInvoices(Receiver.Customer());
        await AssertInvoices(sendInvoicesResult);

        var nowUtc = DateTime.UtcNow;
        var transactions = await NavClient.GetTransactionListAsync(page: 1, insertedFromUtc: nowUtc.AddMinutes(-10), insertedToUtc: nowUtc.AddMinutes(1));
        TestFixture.AssertResponse(transactions);

        Assert.That(transactions.SuccessResult.Transactions.Select(t => t.TransactionId), Does.Contain(sendInvoicesResult.SuccessResult));
    }

    private static Receiver LocalCompanyReceiver()
    {
        return Receiver.LocalCompany(
            taxpayerId: LocalTaxpayerIdentificationNumber.Create("10630433").Success.Get(),
            name: Models.Name.Create("Hungarian test company ltd.").Success.Get(),
            address: CreateAddress(Countries.Hungary)
        );
    }

    private static Receiver ForeignCompanyReceiver(string countryCode, string taxId)
    {
        var country = Countries.GetByCode(countryCode).Get();
        var taxpayerNumber = taxId.AsNonEmpty().Map(i => TaxpayerIdentificationNumber.Create(country, i).Success.Get());
        return Receiver.ForeignCompany(
            name: Models.Name.Create("Foreign test company ltd.").Success.Get(),
            address: CreateAddress(country),
            taxpayerId: taxpayerNumber.GetOrNull()
        ).Success.Get();
    }

    private async Task<ResponseResult<string, ResultErrorCode>> SendInvoices(Receiver receiver, InvoiceNumber invoiceNumber = null, InvoiceCategory category = InvoiceCategory.Normal)
    {
        var exchangeToken = await NavClient.GetExchangeTokenAsync();
        return await NavClient.SendInvoicesAsync(
            token: exchangeToken.SuccessResult,
            invoices: Sequence.FromPreordered(new[] { CreateInvoice(receiver, invoiceNumber, category) }, startIndex: 1).Get()
        );
    }

    private async Task<ResponseResult<string, ResultErrorCode>> SendModificationInvoices(Receiver receiver, InvoiceNumber originalInvoiceNumber, ModificationOperation operation)
    {
        var exchangeToken = await NavClient.GetExchangeTokenAsync();
        var documents = Sequence.FromPreordered(new[] { CreateModificationInvoice(originalInvoiceNumber, receiver, operation) }, startIndex: 1).Get();
        return operation switch
        {
            ModificationOperation.Storno => await NavClient.SendCancellationDocumentsAsync(exchangeToken.SuccessResult, documents),
            _ => await NavClient.SendModificationDocumentsAsync(exchangeToken.SuccessResult, documents)
        };
    }

    private Invoice CreateInvoice(Receiver receiver, InvoiceNumber invoiceNumber, InvoiceCategory category)
    {
        var nowUtc = DateTime.UtcNow.Date;
        // An aggregate invoice needs more than one delivery date, which is the whole reason to declare one.
        var isAggregate = category == InvoiceCategory.Aggregate;
        var items = new[]
        {
            CreateItem(nowUtc, 1694.92m, 2000m, 305.08m, VatRate.Percentage(0.18m).Success.Get(), quantity: 1),
            CreateItem(isAggregate ? nowUtc.AddDays(-1) : nowUtc, 2362.20m, 3000m, 637.8m, VatRate.Percentage(0.27m).Success.Get(), quantity: 1),
            CreateItem(isAggregate ? nowUtc.AddDays(-2) : nowUtc, 952.38m, 1000m, 47.62m, VatRate.Percentage(0.05m).Success.Get(), quantity: 1)
        };

        return Invoice.Create(
            number: invoiceNumber ?? InvoiceNumber.Create($"INVOICE-{Guid.NewGuid()}").Success.Get(),
            category: category,
            issueDate: nowUtc,
            paymentDate: nowUtc,
            supplierInfo: CreateSupplierInfo(),
            receiver: receiver,
            currencyCode: CurrencyCode.Create("HUF").Success.Get(),
            items: Sequence.FromPreordered(items, startIndex: 1).Get(),
            paymentMethod: PaymentMethod.Card
        ).Success.Get();
    }

    private ModificationInvoice CreateModificationInvoice(InvoiceNumber originalDocumentNumber, Receiver receiver, ModificationOperation operation)
    {
        var nowUtc = DateTime.UtcNow.Date;
        var items = new[]
        {
            CreateItem(nowUtc, -1694.92m, -2000m, -305.08m, VatRate.Percentage(0.18m).Success.Get(), quantity: -1),
            CreateItem(nowUtc, -2362.20m, -3000m, -637.8m, VatRate.Percentage(0.27m).Success.Get(), quantity: -1),
            CreateItem(nowUtc, -952.38m, -1000m, -47.62m, VatRate.Percentage(0.05m).Success.Get(), quantity: -1)
        };

        var invoice = Invoice.Create(
            number: InvoiceNumber.Create($"REBATE-{Guid.NewGuid()}").Success.Get(),
            category: InvoiceCategory.Normal,
            issueDate: nowUtc,
            paymentDate: nowUtc,
            supplierInfo: CreateSupplierInfo(),
            receiver: receiver,
            currencyCode: CurrencyCode.Create("HUF").Success.Get(),
            items: Sequence.FromPreordered(items, startIndex: 1).Get(),
            paymentMethod: PaymentMethod.Cash
        ).Success.Get();

        return ModificationInvoice.Create(
            invoice: invoice,
            operation: operation,
            originalDocumentNumber: originalDocumentNumber,
            modificationIndex: 1,
            itemIndexOffset: 3,
            modifyWithoutMaster: false
        ).Success.Get();
    }

    private static InvoiceItem CreateItem(DateTime deliveryDate, decimal net, decimal gross, decimal tax, VatRate vatRate, decimal quantity)
    {
        var amount = new Models.Amount(net: new AmountValue(net), gross: new AmountValue(gross), tax: new AmountValue(tax));
        var amounts = new ItemAmounts(amount, amount, vatRate);
        return new InvoiceItem(
            deliveryDate: deliveryDate,
            totalAmounts: amounts,
            unitAmounts: amounts,
            unitOfMeasure: UnitOfMeasure.Own("Night").Success.Get(),
            description: Description.Create($"Item {vatRate.GetHashCode()} description").Success.Get(),
            quantity: quantity,
            lineExchangeRate: ExchangeRate.Create(1).Success.Get()
        );
    }

    private SupplierInfo CreateSupplierInfo()
    {
        return new SupplierInfo(
            taxpayerId: TestFixture.TaxPayerId,
            vatCode: VatCode.Create("2").Success.Get(),
            name: Models.Name.Create("Supplier company").Success.Get(),
            address: CreateAddress(Countries.Hungary)
        );
    }

    private static SimpleAddress CreateAddress(Country country)
    {
        return new SimpleAddress(
            city: City.Create("Budapest").Success.Get(),
            country: country,
            additionalAddressDetail: AdditionalAddressDetail.Create("Additional address detail").Success.Get(),
            postalCode: PostalCode.Create("1111").Success.Get()
        );
    }

    private async Task AssertInvoices(ResponseResult<string, ResultErrorCode> sendInvoicesResults)
    {
        TestFixture.AssertResponse(sendInvoicesResults);

        await Task.Delay(2000);

        var transactionId = sendInvoicesResults.SuccessResult;
        var transactionStatus = await NavClient.GetTransactionStatusAsync(transactionId);
        TestFixture.AssertResponse(transactionStatus);

        var invoiceStatuses = transactionStatus.SuccessResult.InvoiceStatuses;
        foreach (var status in invoiceStatuses)
        {
            var value = status.Value;
            Assert.That(value.Status, Is.EqualTo(InvoiceState.Done));
        }

        var errorValidationResults = invoiceStatuses.SelectMany(s => s.Value.ValidationResults).Where(r => r.ResultCode.Equals(ValidationResultCode.Error)).ToList();
        Assert.That(errorValidationResults, Is.Empty, "Response contains validation errors.");
    }
}
