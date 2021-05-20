using System.Threading.Tasks;
using NUnit.Framework;

namespace Mews.Fiscalization.Hungary.Tests
{
    [TestFixture]
    public class StatusTests
    {
        [Test]
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