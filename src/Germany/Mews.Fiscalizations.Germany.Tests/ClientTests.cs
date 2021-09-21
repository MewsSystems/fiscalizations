using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Germany.Tests
{
    [TestFixture]
    public class ClientTests
    {
        [Test]
        public async Task GetClientSucceeds()
        {
            var fiskalyClient = TestFixture.GetFiskalyClient();
            var testData = await TestFixture.CreateTestData();
            var result = await fiskalyClient.GetClientAsync(testData.AccessToken, testData.Client.Id, testData.Tss.Id);

            AssertClient(result.IsSuccess, result.SuccessResult.Id);

            await TestFixture.CleanTestData(testData);
        }

        private void AssertClient(bool isSuccess, object value)
        {
            Assert.IsTrue(isSuccess);
            Assert.IsNotNull(value);
        }
    }
}
