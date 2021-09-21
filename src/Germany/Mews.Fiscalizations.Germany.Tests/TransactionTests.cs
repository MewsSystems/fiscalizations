using Mews.Fiscalizations.Germany.Model;
using Mews.Fiscalizations.Germany.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.German.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public async Task StatusCheckSucceeds()
        {
            var fiskalyClient = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var status = await fiskalyClient.GetClientAsync(testData.AccessToken, testData.Client.Id, testData.Tss.Id);

            Assert.IsTrue(status.IsSuccess);

            await TestFixture.CleanTestData(testData);
        }

        [Test, Order(1)]
        public async Task GetTransactionSucceeds()
        {
            var fiskalyClient = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var accessToken = testData.AccessToken;
            var clientId = testData.Client.Id;
            var tssId = testData.Tss.Id;
            var startedTransaction = await fiskalyClient.StartTransactionAsync(accessToken, clientId, tssId, Guid.NewGuid());
            var retrievedStartedTransaction = await fiskalyClient.GetTransactionAsync(accessToken, tssId, startedTransaction.SuccessResult.Id);
            Assert.AreEqual(retrievedStartedTransaction.SuccessResult.State, TransactionState.Active);

            var finishedTransaction = await fiskalyClient.FinishTransactionAsync(accessToken, clientId, tssId, GetBill(), startedTransaction.SuccessResult.Id, lastRevision: "2");
            var retrievedFinishedTransaction = await fiskalyClient.GetTransactionAsync(accessToken, tssId, finishedTransaction.SuccessResult.Id);
            Assert.AreEqual(retrievedFinishedTransaction.SuccessResult.State, TransactionState.Finished);

            await TestFixture.CleanTestData(testData);
        }

        [Test, Order(2)]
        public async Task StartTransactionSucceeds()
        {
            var fiskalyClient = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var accessToken = testData.AccessToken;
            var clientId = testData.Client.Id;
            var tssId = testData.Tss.Id;
            var startedTransaction = await fiskalyClient.StartTransactionAsync(accessToken, clientId, tssId, Guid.NewGuid());
            var successResult = startedTransaction.SuccessResult;

            Assert.IsTrue(startedTransaction.IsSuccess);
            Assert.IsTrue(successResult.StartUtc.HasValue);
            Assert.IsNotNull(successResult.Id);

            await TestFixture.CleanTestData(testData);
        }

        [Test, Order(3)]
        public async Task StartAndFinishTransactionSucceeds()
        {
            var fiskalyClient = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var accessToken = testData.AccessToken;
            var clientId = testData.Client.Id;
            var tssId = testData.Tss.Id;
            var startedTransaction = await fiskalyClient.StartTransactionAsync(accessToken, clientId, tssId, Guid.NewGuid());
            var endedTransaction = await fiskalyClient.FinishTransactionAsync(accessToken, clientId, tssId, GetBill(), startedTransaction.SuccessResult.Id, lastRevision: "2");
            var successResult = endedTransaction.SuccessResult;
            var signature = successResult.Signature;

            Assert.IsTrue(endedTransaction.IsSuccess);
            Assert.IsTrue(successResult.StartUtc.HasValue);
            Assert.IsTrue(successResult.EndUtc.HasValue);
            Assert.IsNotNull(signature);
            Assert.IsNotEmpty(signature.Value);
            Assert.IsNotEmpty(signature.Algorithm);
            Assert.IsNotEmpty(signature.PublicKey);

            await TestFixture.CleanTestData(testData);
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
}