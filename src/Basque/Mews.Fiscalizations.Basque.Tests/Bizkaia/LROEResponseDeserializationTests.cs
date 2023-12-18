using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia;

[TestFixture]
public class LROEResponseDeserializationTests
{
    private const string CorrectResponseFilename = "./Bizkaia/Documents/LROE_240_FacturasEmitidasConSG_Correcta.xml";
    private const string PartiallyCorrectResponseFilename = "./Bizkaia/Documents/LROE_240_FacturasEmitidasConSG_Parc_Correcta.xml";
    private const string IncorrectResponseFilename = "./Bizkaia/Documents/LROE_240_FacturasEmitidasConSG_Incorrecta.xml";
    private const string TicketBaiFilename = "./Bizkaia/Documents/ticketBai.xml";
    private const int NumberOfRecords = 2;

    [Test]
    public void CorrectResponse_Deserialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(CorrectResponseFilename);

            Assert.That(response.Registros.Length == NumberOfRecords);
            Assert.That(response.Registros.All(registro => registro.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Correcto)));
        });
    }

    [Test]
    public void PartiallyCorrectResponse_Deserialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(PartiallyCorrectResponseFilename);

            Assert.That(response.Registros.Length == NumberOfRecords);

            //first registro is correct
            var firstRecord = response.Registros.First();
            Assert.That(firstRecord.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Correcto));

            //second registro is incorrect
            var secondRecord = response.Registros.Last();
            Assert.That(secondRecord.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Incorrecto));
        });
    }

    [Test]
    public void IncorrectResponse_Deserialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            var response = XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(IncorrectResponseFilename);

            Assert.That(response.Registros.Length == NumberOfRecords);
            Assert.That(response.Registros.All(registro => registro.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Incorrecto)));
        });
    }

    [Test]
    public void WrongFileFormat_Deserialization_Fails()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            XmlSerializationHelper.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(TicketBaiFilename);
        });
    }


}
