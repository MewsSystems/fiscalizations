using System.Threading.Tasks;
using NUnit.Framework;

namespace Mews.Fiscalizations.Hungary.Tests
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
        [Ignore("Will be rewritten and re-enabled when upgrading to V3.0.")]
        public async Task StatusCheck()
        {
            var client = TestFixture.GetNavClient();
            var status = await client.GetTransactionStatusAsync("30NKOUNC66LSDD4Z");

            Assert.IsNotNull(status.SuccessResult);
            Assert.IsNull(status.OperationalErrorResult);
            Assert.IsNull(status.GeneralErrorResult);
        }
    }
}