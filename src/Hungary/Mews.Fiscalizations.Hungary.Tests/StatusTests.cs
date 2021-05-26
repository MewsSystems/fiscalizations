using System.Threading.Tasks;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Hungary.Models;
using NUnit.Framework;

namespace Mews.Fiscalizations.Hungary.Tests
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

        [Test]
        public async Task GetTaxerpayerData()
        {
            var client = TestFixture.GetNavClient();
            var taxpayer = TaxpayerIdentificationNumber.Create(Countries.Hungary, "10630433").Success.Get();
            var taxpayerData = await client.GetTaxPayerDataAsync(taxpayer);

            AssertResponse(taxpayerData);
        }

        [Test]
        public async Task GetExchangeTokenSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();

            AssertResponse(exchangeToken);
        }

        private void AssertResponse<TResult, TCode>(ResponseResult<TResult, TCode> responseResult)
            where TResult : class
            where TCode : struct
        {
            Assert.IsNotNull(responseResult.SuccessResult);
            Assert.IsNull(responseResult.GeneralErrorResult);
            Assert.IsNull(responseResult.OperationalErrorResult);
        }
    }
}