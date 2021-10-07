using Mews.Fiscalizations.Germany.V2.Model;
using NUnit.Framework;
using System.Linq;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests.V2
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public async Task CreateClientSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

            await client.AdminLoginAsync(accessToken, TestFixture.TssId, TestFixture.AdminPin);
            var createdClient = await client.CreateClientAsync(accessToken, TestFixture.TssId);

            AssertClient(createdClient.IsSuccess, createdClient.SuccessResult.Id);

            // Disabling the Client so we don't exceed the test environment limit.
            await client.UpdateClientAsync(accessToken, TestFixture.TssId, createdClient.SuccessResult.Id, ClientState.Deregistered);
        }

        [Test]
        public async Task GetClientSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
            var result = await client.GetClientAsync(accessToken, TestFixture.ClientId, TestFixture.TssId);

            AssertClient(result.IsSuccess, result.SuccessResult.Id);
        }

        [Test]
        public async Task GetAllTssClientsSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

            await client.AdminLoginAsync(accessToken, TestFixture.TssId, TestFixture.AdminPin);
            var createdClient = (await client.CreateClientAsync(accessToken, TestFixture.TssId)).SuccessResult;
            var result = await client.GetAllEnabledTSSsAsync(accessToken);

            Assert.IsTrue(result.SuccessResult.Select(r => r.Id).Contains(createdClient.Id));

            // Disabling the Client so we don't exceed the test environment limit.
            await client.UpdateClientAsync(accessToken, TestFixture.TssId, createdClient.Id, ClientState.Deregistered);
        }

        private void AssertClient(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}