using FuncSharp;
using Mews.Fiscalizations.Basque.Model;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Basque.Tests
{
    [TestFixture]
    public class SendInvoiceTests
    {
        [Test]
        [TestCase(Region.Alaba, false, false, TestName = "Alaba - Send invoice with local receiver")]
        [TestCase(Region.Alaba, true, false, TestName = "Alaba - Send invoice with foreign receiver")]
        [TestCase(Region.Alaba, false, true, TestName = "Alaba - Send negative invoice with local receiver")]
        [TestCase(Region.Alaba, true, true, TestName = "Alaba - Send negative invoice with foreign receiver")]
        [TestCase(Region.Gipuzkoa, false, false, TestName = "Gipuzkoa - Send invoice with local receiver")]
        [TestCase(Region.Gipuzkoa, true, false, TestName = "Gipuzkoa - Send invoice with foreign receiver")]
        [TestCase(Region.Gipuzkoa, false, true, TestName = "Gipuzkoa - Send negative invoice with local receiver")]
        [TestCase(Region.Gipuzkoa, true, true, TestName = "Gipuzkoa - Send negative invoice with foreign receiver")]
        [Retry(3)]
        public async Task SendSimpleInvoiceSucceeds(Region region, bool localReceivers, bool negativeInvoice)
        {
            var testFixture = new TestFixture(region);
            var request = InvoiceTestData.CreateInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers, negativeInvoice);
            var response = await testFixture.Client.SendInvoiceAsync(request);
            TestFixture.AssertResponse(region, response);
        }

        [Test]
        [TestCase(Region.Alaba, TestName = "Alaba - Invoice chaining")]
        [TestCase(Region.Gipuzkoa, TestName = "Gipuzkoa - Invoice chaining")]
        [Retry(3)]
        public async Task SendChainedInvoiceSucceeds(Region region)
        {
            var testFixture = new TestFixture(region);
            var request1 = InvoiceTestData.CreateInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers: true, negativeInvoice: false);
            var response1 = await testFixture.Client.SendInvoiceAsync(request1);
            TestFixture.AssertResponse(region, response1);

            var originalInvoiceHeader = request1.Invoice.Header;
            var request2 = InvoiceTestData.CreateInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers: true, negativeInvoice: false, originalInvoiceInfo: new OriginalInvoiceInfo(
                number: originalInvoiceHeader.Number,
                issueDate: originalInvoiceHeader.Issued,
                signature: response1.SignatureValue,
                series: originalInvoiceHeader.Series.GetOrNull()
            ));
            var response2 = await testFixture.Client.SendInvoiceAsync(request2);
            TestFixture.AssertResponse(region, response2);
        }
    }
}