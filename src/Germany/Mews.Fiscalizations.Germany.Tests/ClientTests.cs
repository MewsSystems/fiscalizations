﻿using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public async Task CreateClientSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync().ConfigureAwait(continueOnCapturedContext: false)).SuccessResult;
            var createdClient = await client.CreateClientAsync(accessToken, TestFixture.TssId).ConfigureAwait(continueOnCapturedContext: false);

            AssertClient(createdClient.IsSuccess, createdClient.SuccessResult.Id);
        }

        [Test]
        public async Task GetClientSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync().ConfigureAwait(continueOnCapturedContext: false)).SuccessResult;
            var result = await client.GetClientAsync(accessToken, TestFixture.ClientId, TestFixture.TssId).ConfigureAwait(continueOnCapturedContext: false);

            AssertClient(result.IsSuccess, result.SuccessResult.Id);
        }

        private void AssertClient(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}
