using Mews.Fiscalizations.Germany.V2.Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests.V2;

[TestFixture]
public class TransactionTests
{
    [Test]
    public async Task StatusCheckSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
        var status = await client.GetClientAsync(accessToken, data.Client.Id, data.Tss.Id);

        Assert.IsTrue(status.IsSuccess);
    }

    [Test, Order(1)]
    public async Task GetTransactionSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var tssId = data.Tss.Id;
        var clientId = data.Client.Id;
        var accessToken = await client.GetAccessTokenAsync();
        var startedTransaction = await client.StartTransactionAsync(accessToken.SuccessResult, clientId, tssId, Guid.NewGuid());
        var retrievedStartedTransaction = await client.GetTransactionAsync(accessToken.SuccessResult, tssId, startedTransaction.SuccessResult.Id);
        Assert.AreEqual(retrievedStartedTransaction.SuccessResult.State, TransactionState.Active);

        var finishedTransaction = await client.FinishTransactionAsync(accessToken.SuccessResult, clientId, tssId, GetBill(), startedTransaction.SuccessResult.Id);
        var retrievedFinishedTransaction = await client.GetTransactionAsync(accessToken.SuccessResult, tssId, finishedTransaction.SuccessResult.Id);
        Assert.AreEqual(retrievedFinishedTransaction.SuccessResult.State, TransactionState.Finished);
    }

    [Test, Order(2)]
    public async Task StartTransactionSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var accessToken = await client.GetAccessTokenAsync();
        var startedTransaction = await client.StartTransactionAsync(accessToken.SuccessResult, data.Client.Id, data.Tss.Id, Guid.NewGuid());
        var successResult = startedTransaction.SuccessResult;

        Assert.IsTrue(startedTransaction.IsSuccess);
        Assert.IsTrue(successResult.StartUtc.HasValue);
        Assert.IsNotNull(successResult.Id);
    }

    [Test, Order(3)]
    public async Task StartAndFinishTransactionSucceeds()
    {
        var data = TestFixture.FiskalyTestData;
        var client = data.FiskalyClient;
        var clientId = data.Client.Id;
        var tssId = data.Tss.Id;
        var accessToken = await client.GetAccessTokenAsync();
        var successAccessTokenResult = accessToken.SuccessResult;
        var startedTransaction = await client.StartTransactionAsync(successAccessTokenResult, clientId, tssId, Guid.NewGuid());
        var endedTransaction = await client.FinishTransactionAsync(successAccessTokenResult, clientId, tssId, GetBill(), startedTransaction.SuccessResult.Id);
        var successResult = endedTransaction.SuccessResult;
        var signature = successResult.Signature;

        Assert.IsTrue(endedTransaction.IsSuccess);
        Assert.IsTrue(successResult.StartUtc.HasValue);
        Assert.IsTrue(successResult.EndUtc.HasValue);
        Assert.IsNotNull(signature);
        Assert.IsNotEmpty(signature.Value);
        Assert.IsNotEmpty(signature.Algorithm);
        Assert.IsNotEmpty(signature.PublicKey);
    }

    private Bill GetBill()
    {
        return new Bill(
            type: BillType.Receipt,
            payments: GetPayments(),
            items: GetItems()
        );
    }

    private List<Payment> GetPayments()
    {
        return new List<Payment>()
        {
            new Payment(25, PaymentType.Cash, "EUR"),
            new Payment(30, PaymentType.NonCash, "EUR")
        };
    }

    private List<Item> GetItems()
    {
        return new List<Item>()
        {
            new Item(20, VatRateType.Normal),
            new Item(30, VatRateType.Normal),
            new Item(5, VatRateType.Reduced)
        };
    }
}