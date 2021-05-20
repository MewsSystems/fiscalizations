using Mews.Fiscalization.Germany.Model;
using Mews.Fiscalization.Germany.Tests;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mews.Fiscalization.German.Tests
{
    [TestFixture]
    public class TransactionTests
    {
        [Test]
        public async Task StatusCheckSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
            var status = await client.GetClientAsync(accessToken, TestFixture.ClientId, TestFixture.TssId);

            Assert.IsTrue(status.IsSuccess);
        }

        [Test]
        public async Task StartTransactionSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = await client.GetAccessTokenAsync();
            var startedTransaction = await client.StartTransactionAsync(accessToken.SuccessResult, TestFixture.ClientId, TestFixture.TssId, Guid.NewGuid());
            var successResult = startedTransaction.SuccessResult;

            Assert.IsTrue(startedTransaction.IsSuccess);
            Assert.IsTrue(successResult.StartUtc.HasValue);
            Assert.IsNotNull(successResult.Id);
        }

        [Test]
        public async Task StartAndFinishTransactionSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var clientId = TestFixture.ClientId;
            var tssId = TestFixture.TssId;
            var accessToken = await client.GetAccessTokenAsync();
            var successAccessTokenResult = accessToken.SuccessResult;
            var startedTransaction = await client.StartTransactionAsync(successAccessTokenResult, clientId, tssId, Guid.NewGuid());
            var endedTransaction = await client.FinishTransactionAsync(successAccessTokenResult, clientId, tssId, GetBill(), startedTransaction.SuccessResult.Id, lastRevision: "1");
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
}