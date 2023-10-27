using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia;

[TestFixture]
public class DtoToModelConverterTests
{
    private const string CorrectResponseFilename = "./Bizkaia/Documents/ResponseTest_Correcta.xml";
    private const string IncorrectResponseFilename = "./Bizkaia/Documents/ResponseTest_Incorrecta.xml";

    [Test]
    public void CorrectResponse_Generates_CorrectResult()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(CorrectResponseFilename);

            var sendInvoiceResponse = DtoToModelConverter.Convert(response, string.Empty, string.Empty, string.Empty, String1To100.CreateUnsafe("somefakesignature"));

            Assert.IsTrue(sendInvoiceResponse.State == InvoiceState.Received);
        });
    }

    [Test]
    public void IncorrectReponse_Generates_IncorrectResponse()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(IncorrectResponseFilename);

            var sendInvoiceResponse = DtoToModelConverter.Convert(response, string.Empty, string.Empty, string.Empty, String1To100.CreateUnsafe("somefakesignature"));

            Assert.IsTrue(sendInvoiceResponse.State == InvoiceState.Refused);
            Assert.IsTrue(sendInvoiceResponse.ValidationResults.Flatten().First().ErrorCode == ErrorCode.DuplicateInvoice);
        });
    }
}
