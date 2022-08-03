using FuncSharp;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Basque.Tests
{
    [TestFixture]
    public class SendInvoiceTests : TestFixture
    {
        [Test]
        public async Task SendSimpleInvoiceSucceeds()
        {
            var request = CreateInvoiceRequest();
            var response = await Client.SendInvoiceAsync(request);

            // TODO: Assert the Qr code uri content, make sure it contains all the required params/data.
            Assert.IsEmpty(response.ValidationResults.Flatten());
            Assert.IsNotEmpty(response.QrCodeUri);
            Assert.IsNotEmpty(response.TBAIIdentifier);
            Assert.IsNotEmpty(response.XmlRequestContent);
        }
    }
}