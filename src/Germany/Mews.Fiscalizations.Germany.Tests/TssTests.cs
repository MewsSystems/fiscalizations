using Mews.Fiscalizations.Germany.V2.Model;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests
{
    [TestFixture]
    public class TssTests
    {
        [Test]
        public async Task CreateTssSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
            var createdTss = await client.CreateTssAsync(accessToken);

            AssertTss(createdTss.IsSuccess, createdTss.SuccessResult.Id);

            // In order to disable a TSS, it must be in Uninitialized state.
            await client.UpdateTssAsync(accessToken, createdTss.SuccessResult.Id, TssState.Uninitialized);

            var newPin = "1234567890";
            await client.ChangeAdminPinAsync(accessToken, createdTss.SuccessResult.Id, createdTss.SuccessResult.AdminPuk, newPin);
            await client.AdminLoginAsync(accessToken, createdTss.SuccessResult.Id, newPin);

            // Disabling the TSS after creation so we don't exceed the test environment limit.
            await client.UpdateTssAsync(accessToken, createdTss.SuccessResult.Id, TssState.Disabled);
        }

        [Test]
        public async Task GetTssSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
            var tss = await client.GetTssAsync(accessToken, TestFixture.TssId);

            AssertTss(tss.IsSuccess, tss.SuccessResult.Id);
        }

        private void AssertTss(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}