using FuncSharp;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Basque.Tests
{
    [TestFixture]
    public class SendInvoiceTests
    {
        [Test]
        [Retry(3)]
        public async Task SendSimpleInvoiceSucceeds()
        {
            var request = TestFixture.CreateInvoiceRequest();
            var response = await TestFixture.Client.SendInvoiceAsync(request);

            Assert.IsEmpty(response.ValidationResults.Flatten());
            Assert.IsNotEmpty(response.QrCodeUri);
            Assert.IsNotEmpty(response.TBAIIdentifier);
            Assert.IsNotEmpty(response.XmlRequestContent);
        }
    }
}