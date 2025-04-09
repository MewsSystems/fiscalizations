namespace Mews.Fiscalizations.Basque.Tests;

[TestFixture]
public class SendInvoiceTests
{
    [Ignore("Disabling temporarily")]
    [Test]
    [TestCase(Region.Araba, true, false, TestName = "Araba - Send complete invoice with local receiver")]
    [TestCase(Region.Araba, false, false, TestName = "Araba - Send complete invoice with foreign receiver")]
    [TestCase(Region.Araba, true, true, TestName = "Araba - Send negative complete invoice with local receiver")]
    [TestCase(Region.Araba, false, true, TestName = "Araba - Send negative complete invoice with foreign receiver")]
    [TestCase(Region.Gipuzkoa, true, false, TestName = "Gipuzkoa - Send complete invoice with local receiver")]
    [TestCase(Region.Gipuzkoa, false, false, TestName = "Gipuzkoa - Send complete invoice with foreign receiver")]
    [TestCase(Region.Gipuzkoa, true, true, TestName = "Gipuzkoa - Send complete negative invoice with local receiver")]
    [TestCase(Region.Gipuzkoa, false, true, TestName = "Gipuzkoa - Send complete negative invoice with foreign receiver")]
    [TestCase(Region.Bizkaia, true, false, TestName = "Bizkaia - Send complete invoice with local receiver")]
    [TestCase(Region.Bizkaia, false, false, TestName = "Bizkaia - Send complete invoice with foreign receiver")]
    [TestCase(Region.Bizkaia, true, true, TestName = "Bizkaia - Send complete negative invoice with local receiver")]
    [TestCase(Region.Bizkaia, false, true, TestName = "Bizkaia - Send complete negative invoice with foreign receiver")]
    [Retry(3)]
    public async Task SendCompleteInvoiceSucceeds(Region region, bool localReceivers, bool negativeInvoice)
    {
        var testFixture = new TestFixture(region);
        var client = testFixture.Client;
        var request = InvoiceTestData.CreateCompleteInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers, negativeInvoice);
        var tbaiData = client.GetTicketBaiInvoiceData(request);
        var response = await client.SendInvoiceAsync(tbaiData);
        TestFixture.AssertResponse(region, response, tbaiData);
    }

    [Ignore("Disabling temporarily")]
    [TestCase(Region.Araba, false, TestName = "Araba - Send simplified invoice")]
    [TestCase(Region.Araba, true, TestName = "Araba - Send simplified negative invoice")]
    [TestCase(Region.Gipuzkoa, false, TestName = "Gipuzkoa - Send simplified invoice")]
    [TestCase(Region.Gipuzkoa, true, TestName = "Gipuzkoa - Send simplified negative invoice")]
    [TestCase(Region.Bizkaia, false, TestName = "Bizkaia - Send simplified invoice")]
    [TestCase(Region.Bizkaia, true, TestName = "Bizkaia - Send simplified negative invoice")]
    [Retry(3)]
    public async Task SendSimplifiedInvoiceSucceeds(Region region, bool negativeInvoice)
    {
        var testFixture = new TestFixture(region);
        var client = testFixture.Client;
        var request = InvoiceTestData.CreateSimplifiedInvoiceRequest(testFixture.Issuer, testFixture.Software, negativeInvoice);
        var tbaiData = client.GetTicketBaiInvoiceData(request);
        var response = await client.SendInvoiceAsync(tbaiData);
        TestFixture.AssertResponse(region, response, tbaiData);
    }

    [Ignore("Disabling temporarily")]
    [Test]
    [TestCase(Region.Araba, false, TestName = "Araba - Complete Invoice chaining")]
    [TestCase(Region.Araba, true, TestName = "Araba - Simplified Invoice chaining")]
    [TestCase(Region.Gipuzkoa, false, TestName = "Gipuzkoa - Complete Invoice chaining")]
    [TestCase(Region.Gipuzkoa, true, TestName = "Gipuzkoa - Simplified Invoice chaining")]
    [TestCase(Region.Bizkaia, false, TestName = "Bizkaia - Complete Invoice chaining")]
    [TestCase(Region.Bizkaia, true, TestName = "Bizkaia - Simplified Invoice chaining")]
    [Retry(3)]
    public async Task SendChainedInvoiceSucceeds(Region region, bool isSimplified)
    {
        var testFixture = new TestFixture(region);
        var client = testFixture.Client;
        var request1 = isSimplified.Match(
            t => InvoiceTestData.CreateSimplifiedInvoiceRequest(testFixture.Issuer, testFixture.Software, negativeInvoice: false),
            f => InvoiceTestData.CreateCompleteInvoiceRequest(testFixture.Issuer, testFixture.Software, negativeInvoice: false, localReceivers: true)
        );
        var tbaiData1 = client.GetTicketBaiInvoiceData(request1);
        var response1 = await client.SendInvoiceAsync(tbaiData1);
        TestFixture.AssertResponse(region, response1, tbaiData1);

        var originalInvoiceHeader = request1.Invoice.Header;
        var originalInvoice = new OriginalInvoiceInfo(
            number: originalInvoiceHeader.Number,
            issueDate: originalInvoiceHeader.Issued,
            signature: response1.SignatureValue,
            series: originalInvoiceHeader.Series.GetOrNull()
        );
        var request2 = isSimplified.Match(
            t => InvoiceTestData.CreateSimplifiedInvoiceRequest(testFixture.Issuer, testFixture.Software, negativeInvoice: false, originalInvoice),
            f => InvoiceTestData.CreateCompleteInvoiceRequest(testFixture.Issuer, testFixture.Software, localReceivers: true, negativeInvoice: false, originalInvoice)
        );
        var tbaiData2 = client.GetTicketBaiInvoiceData(request2);
        var response2 = await client.SendInvoiceAsync(tbaiData2);
        TestFixture.AssertResponse(region, response2, tbaiData2);
    }
}