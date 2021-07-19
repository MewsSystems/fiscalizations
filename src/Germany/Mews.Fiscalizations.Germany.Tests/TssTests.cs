using Mews.Fiscalizations.Germany.Model;
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
            var accessToken = (await client.GetAccessTokenAsync().ConfigureAwait(continueOnCapturedContext: false)).SuccessResult;
            var createdTss = await client.CreateTssAsync(accessToken, TssState.Initialized, description: "Creating a test TSS.").ConfigureAwait(continueOnCapturedContext: false);

            AssertTss(createdTss.IsSuccess, createdTss.SuccessResult.Id);
        }

        [Test]
        public async Task GetTssSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var accessToken = (await client.GetAccessTokenAsync().ConfigureAwait(continueOnCapturedContext: false)).SuccessResult;
            var tss = await client.GetTssAsync(accessToken, TestFixture.TssId).ConfigureAwait(continueOnCapturedContext: false);

            AssertTss(tss.IsSuccess, tss.SuccessResult.Id);
        }

        private void AssertTss(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}
