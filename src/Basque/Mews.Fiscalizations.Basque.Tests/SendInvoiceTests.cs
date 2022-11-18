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
        [TestCase(Region.Araba, false, false, TestName = "Araba - Send invoice with local receiver")]
        [TestCase(Region.Araba, true, false, TestName = "Araba - Send invoice with foreign receiver")]
        [TestCase(Region.Araba, false, true, TestName = "Araba - Send negative invoice with local receiver")]
        [TestCase(Region.Araba, true, true, TestName = "Araba - Send negative invoice with foreign receiver")]
        [TestCase(Region.Gipuzkoa, false, false, TestName = "Gipuzkoa - Send invoice with local receiver")]
        [TestCase(Region.Gipuzkoa, true, false, TestName = "Gipuzkoa - Send invoice with foreign receiver")]
        [TestCase(Region.Gipuzkoa, false, true, TestName = "Gipuzkoa - Send negative invoice with local receiver")]
        [TestCase(Region.Gipuzkoa, true, true, TestName = "Gipuzkoa - Send negative invoice with foreign receiver")]
        [Retry(3)]
        public async Task SendSimpleInvoiceSucceeds(Region region, bool localReceivers, bool negativeInvoice)
        {
            var testFixture = new TestFixture(region);
            var client = testFixture.Client;
            var request = InvoiceTestData.CreateInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers, negativeInvoice);
            var tbaiData = client.GetTicketBaiInvoiceData(request);
            var response = await client.SendInvoiceAsync(tbaiData);
            TestFixture.AssertResponse(region, response, tbaiData);
        }

        [Test]
        [TestCase(Region.Araba, TestName = "Araba - Invoice chaining")]
        [TestCase(Region.Gipuzkoa, TestName = "Gipuzkoa - Invoice chaining")]
        [Retry(3)]
        public async Task SendChainedInvoiceSucceeds(Region region)
        {
            var testFixture = new TestFixture(region);
            var client = testFixture.Client;
            var request1 = InvoiceTestData.CreateInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers: true, negativeInvoice: false);
            var tbaiData1 = client.GetTicketBaiInvoiceData(request1);
            var response1 = await client.SendInvoiceAsync(tbaiData1);
            TestFixture.AssertResponse(region, response1, tbaiData1);

            var originalInvoiceHeader = request1.Invoice.Header;
            var request2 = InvoiceTestData.CreateInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers: true, negativeInvoice: false, originalInvoiceInfo: new OriginalInvoiceInfo(
                number: originalInvoiceHeader.Number,
                issueDate: originalInvoiceHeader.Issued,
                signature: response1.SignatureValue,
                series: originalInvoiceHeader.Series.GetOrNull()
            ));
            var tbaiData2 = client.GetTicketBaiInvoiceData(request2);
            var response2 = await client.SendInvoiceAsync(tbaiData2);
            TestFixture.AssertResponse(region, response2, tbaiData2);
        }
    }
}