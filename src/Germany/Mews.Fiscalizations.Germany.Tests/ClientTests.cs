using NUnit.Framework;
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
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

            // login
            await client.AdminLoginAsync(accessToken, TestFixture.TssId, "1234567890");
            var createdClient = await client.CreateClientAsync(accessToken, TestFixture.TssId);

            AssertClient(createdClient.IsSuccess, createdClient.SuccessResult.Id);
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
        public async Task UpdateClientSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

            // login
            await client.AdminLoginAsync(accessToken, TestFixture.TssId, "1234567890");
            var result = await client.UpdateClientAsync(accessToken, TestFixture.TssId, TestFixture.ClientId, Model.ClientState.Registered);

            AssertClient(result.IsSuccess, result.SuccessResult.Id);
            Assert.AreEqual(result.SuccessResult.State, Model.ClientState.Registered);
        }

        private void AssertClient(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}
