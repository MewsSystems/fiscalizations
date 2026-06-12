using Mews.Fiscalizations.Fiskaly.APIClients;
using Mews.Fiscalizations.Fiskaly.Models;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashPointClosings;
using Mews.Fiscalizations.Fiskaly.Models.DSFinVK.CashRegisters;
using NUnit.Framework;

namespace Mews.Fiscalizations.Fiskaly.Tests.DSFinVK;

[TestFixture]
public class CashPointClosingTests
{
    private DsfinvkApiClient _client;
    private AccessToken _accessToken;
    private Guid _insertedClosingId;

    [OneTimeSetUp]
    public async Task SetUp()
    {
        _client = new DsfinvkApiClient(
            new HttpClient(),
            TestFixture.DsfinvkApiKey,
            TestFixture.DsfinvkApiSecret
        );
        var tokenResult = await _client.GetAccessTokenAsync();
        _accessToken = tokenResult.SuccessResult;

        // A closing references the cash register's client UUID, so the register must exist first. The
        // upsert is idempotent; doing it here keeps these tests independent of execution order.
        await _client.UpsertCashRegisterAsync(_accessToken, TestFixture.DsfinvkTestClientId, CreateCashRegister());

        var closing = BuildTestClosing(exportId: Math.Abs(Guid.NewGuid().GetHashCode()));
        var insertResult = await _client.InsertCashPointClosingAsync(_accessToken, closing);
        _insertedClosingId = insertResult.IsSuccess ? closing.ClosingId : Guid.Empty;
    }

    [Test]
    public async Task InsertCashPointClosingSucceeds()
    {
        var closing = BuildTestClosing(exportId: Math.Abs(Guid.NewGuid().GetHashCode()));
        var result = await _client.InsertCashPointClosingAsync(_accessToken, closing);

        Assert.That(
            result.IsSuccess,
            $"[{result.ErrorResult?.Status}] {result.ErrorResult?.Error} :: {result.ErrorResult?.Message}"
        );
        Assert.That(
            result.SuccessResult.State,
            Is.AnyOf(
                CashPointClosingState.Pending,
                CashPointClosingState.Working,
                CashPointClosingState.Completed
            )
        );
    }

    [Test]
    public async Task InsertCashPointClosingWithoutTransactionsSucceeds()
    {
        var closing = BuildEmptyTestClosing(exportId: Math.Abs(Guid.NewGuid().GetHashCode()));
        var result = await _client.InsertCashPointClosingAsync(_accessToken, closing);

        Assert.That(
            result.IsSuccess,
            $"[{result.ErrorResult?.Status}] {result.ErrorResult?.Error} :: {result.ErrorResult?.Message}"
        );
        Assert.That(
            result.SuccessResult.State,
            Is.AnyOf(
                CashPointClosingState.Pending,
                CashPointClosingState.Working,
                CashPointClosingState.Completed
            )
        );
    }

    [Test]
    public async Task GetCashPointClosingSucceeds()
    {
        Assert.That(
            _insertedClosingId,
            Is.Not.EqualTo(Guid.Empty),
            "Setup failed to insert a closing."
        );

        var result = await _client.GetCashPointClosingAsync(_accessToken, _insertedClosingId);

        Assert.That(result.IsSuccess, result.ErrorResult?.Message);
        Assert.That(result.SuccessResult.Id, Is.EqualTo(_insertedClosingId));
        Assert.That(result.SuccessResult.ClientId, Is.EqualTo(TestFixture.DsfinvkTestClientId));
    }

    [Test]
    public async Task DeleteCashPointClosingSucceeds()
    {
        var closing = BuildTestClosing(exportId: Math.Abs(Guid.NewGuid().GetHashCode()));
        var insertResult = await _client.InsertCashPointClosingAsync(_accessToken, closing);
        Assert.That(
            insertResult.IsSuccess,
            "Insert before delete failed: " + insertResult.ErrorResult?.Message
        );

        var result = await _client.DeleteCashPointClosingAsync(_accessToken, closing.ClosingId);

        Assert.That(result.IsSuccess, result.ErrorResult?.Message);
    }

    private static CashRegister CreateCashRegister()
    {
        return new CashRegister(
            ClientId: TestFixture.DsfinvkTestClientId,
            Type: CashRegisterType.Master,
            TssId: TestFixture.DsfinvkTestTssId,
            SerialNumber: null,
            Brand: "Mews",
            Model: "Mews PMS",
            SoftwareBrand: "Mews",
            SoftwareVersion: "1.0.0",
            BaseCurrencyCode: "EUR"
        );
    }

    private static CashPointClosing BuildEmptyTestClosing(long exportId)
    {
        return new CashPointClosing(
            ClosingId: Guid.NewGuid(),
            ClientId: TestFixture.DsfinvkTestClientId,
            CashPointClosingExportId: exportId,
            ExportCreationDate: DateTimeOffset.UtcNow,
            BusinessDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
            FirstTransactionExportId: null,
            LastTransactionExportId: null,
            Transactions: Array.Empty<CashPointClosingTransaction>(),
            CashStatement: null
        );
    }

    private static CashPointClosing BuildTestClosing(long exportId)
    {
        var amountsPerVat = new[]
        {
            new AmountPerVat(
                VatDefinitionExportId: 1,
                GrossAmount: 119.00m,
                NetAmount: 100.00m,
                TaxAmount: 19.00m
            ),
        };

        var transaction = new CashPointClosingTransaction(
            TxId: Guid.NewGuid(),
            TransactionExportId: $"MEWS-{exportId}",
            Number: exportId,
            TimestampStart: DateTimeOffset.UtcNow.AddMinutes(-5),
            TimestampEnd: DateTimeOffset.UtcNow,
            Storno: false,
            ProcessType: ProcessType.Receipt,
            ClosingClientId: TestFixture.DsfinvkTestClientId,
            FullAmountInclVat: 119.00m,
            Lines: new[]
            {
                new TransactionLine(
                    LineItemExportId: "1",
                    Storno: false,
                    BusinessTransactionType: BusinessTransactionType.Umsatz,
                    BusinessCaseAmountsPerVat: amountsPerVat,
                    ItemText: "Room"
                ),
            },
            AmountsPerVat: amountsPerVat,
            PaymentTypes: new[] { new PaymentTypeAmount(PaymentType.NonCash, 119.00m, "EUR") },
            Security: new TransactionSecurity(TssTxId: Guid.NewGuid())
        );

        return new CashPointClosing(
            ClosingId: Guid.NewGuid(),
            ClientId: TestFixture.DsfinvkTestClientId,
            CashPointClosingExportId: exportId,
            ExportCreationDate: DateTimeOffset.UtcNow,
            BusinessDate: DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-1)),
            FirstTransactionExportId: transaction.TransactionExportId,
            LastTransactionExportId: transaction.TransactionExportId,
            Transactions: new[] { transaction },
            CashStatement: new CashStatement(
                BusinessCases: new[]
                {
                    new BusinessCaseSummary(BusinessTransactionType.Umsatz, amountsPerVat),
                },
                Payment: new CashStatementPayment(
                    FullAmount: 119.00m,
                    CashAmount: 0m,
                    CashAmountsByCurrency: Enumerable.Empty<CurrencyAmount>(),
                    PaymentTypes: new[]
                    {
                        new PaymentTypeAmount(PaymentType.NonCash, 119.00m, "EUR"),
                    }
                )
            )
        );
    }
}
