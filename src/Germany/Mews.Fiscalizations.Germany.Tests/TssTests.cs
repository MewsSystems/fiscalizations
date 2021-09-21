using Mews.Fiscalizations.Germany.Model;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests
{
    [TestFixture]
    public class TssTests
    {
        [Test]
        public async Task GetTssSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;
            var tss = await client.GetTssAsync(accessToken, TestFixture.TssId);

            AssertTss(tss.IsSuccess, tss.SuccessResult.Id);
        }

        [Test]
        public async Task UpdateTssSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync()).SuccessResult;

            // login
            await client.AdminLoginAsync(accessToken, TestFixture.TssId, "1234567890");
            var state1 = await client.UpdateTssAsync(accessToken, TestFixture.TssId, TssState.Uninitialized);
            var state2 = await client.UpdateTssAsync(accessToken, TestFixture.TssId, TssState.Initialized);
            AssertTss(state2.IsSuccess, state2.SuccessResult.Id);
        }

        private void AssertTss(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}
