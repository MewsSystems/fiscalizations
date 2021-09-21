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
            var fiskalyClient = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var tss = await fiskalyClient.GetTssAsync(testData.AccessToken, testData.Tss.Id);

            AssertTss(tss.IsSuccess, tss.SuccessResult.Id);

            await TestFixture.CleanTestData(testData);
        }

        [Test]
        public async Task UpdateTssSucceeds()
        {
            var client = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var disabledTss = await client.UpdateTssAsync(testData.AccessToken, testData.Tss.Id, TssState.Disabled);
            AssertTss(disabledTss.IsSuccess, disabledTss.SuccessResult.Id);

            await TestFixture.CleanTestData(testData);
        }

        private void AssertTss(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}
