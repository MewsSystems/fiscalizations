using System.Threading.Tasks;
using Mews.Fiscalizations.Core.Model;
using NUnit.Framework;

namespace Mews.Fiscalizations.Hungary.Tests
{
    [TestFixture]
    public class StatusTests
    {
        private static readonly NavClient NavClient = TestFixture.GetNavClient();

        [Test]
        public async Task GetTransactionStatusSucceeds()
        {
            var status = await NavClient.GetTransactionStatusAsync("30NKOUNC66LSDD4Z");
            TestFixture.AssertResponse(status);
        }

        [Test]
        public async Task GetTaxerpayerDataSucceeds()
        {
            var taxpayer = TaxpayerIdentificationNumber.Create(Countries.Hungary, "10630433", isCountryCodePrefixAllowed: false).Success.Get();
            var taxpayerData = await NavClient.GetTaxPayerDataAsync(taxpayer);
            TestFixture.AssertResponse(taxpayerData);
        }

        [Test]
        public async Task GetExchangeTokenSucceeds()
        {
            var exchangeToken = await NavClient.GetExchangeTokenAsync();
            TestFixture.AssertResponse(exchangeToken);
        }
    }
}