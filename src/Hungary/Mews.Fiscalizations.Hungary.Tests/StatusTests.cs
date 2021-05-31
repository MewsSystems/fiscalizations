using System.Threading.Tasks;
using Mews.Fiscalizations.Core.Model;
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
            TestFixture.AssertResponse(status);
        }

        [Test]
        public async Task GetTaxerpayerData()
        {
            var client = TestFixture.GetNavClient();
            var taxpayer = TaxpayerIdentificationNumber.Create(Countries.Hungary, "10630433").Success.Get();
            var taxpayerData = await client.GetTaxPayerDataAsync(taxpayer);
            TestFixture.AssertResponse(taxpayerData);
        }

        [Test]
        public async Task GetExchangeTokenSucceeds()
        {
            var navClient = TestFixture.GetNavClient();
            var exchangeToken = await navClient.GetExchangeTokenAsync();
            TestFixture.AssertResponse(exchangeToken);
        }
    }
}