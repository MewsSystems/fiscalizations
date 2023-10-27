using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia;

[TestFixture]
public class DtoToModelConverterTests
{
    private const string CorrectResponseFilename = "./Bizkaia/Documents/ResponseTest_Correct.xml";
    private const string IncorrectResponseFilename = "./Bizkaia/Documents/ResponseTest_Incorrect.xml";

    [Test]
    public void CorrectResponse_Generates_CorrectResult()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(CorrectResponseFilename);
            var state = DtoToModelConverter.ConvertBizkaiaState(response.Registros.Single().SituacionRegistro.EstadoRegistro);

            Assert.AreEqual(state, InvoiceState.Received);
        });
    }

    [Test]
    public void IncorrectResponse_Generates_IncorrectResponse()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(IncorrectResponseFilename);
            var errorCode = DtoToModelConverter.ConvertBizkaiaErrorCodes(response.Registros.Single().SituacionRegistro.CodigoErrorRegistro);
            var state = DtoToModelConverter.ConvertBizkaiaState(response.Registros.Single().SituacionRegistro.EstadoRegistro);

            Assert.AreEqual(state, InvoiceState.Refused);
            Assert.AreEqual(errorCode, ErrorCode.DuplicateInvoice);
        });
    }
}
