using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Microsoft.VisualBasic;

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
            LROEPJ240FacturasEmitidasConSGAltaRespuesta response = XmlSerializationHelper.
            Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(CorrectResponseFilename);

            Assert.IsNotNull(response);
            Assert.True(response.Registros.Length == NumberOfRecords);

            Assert.True(response.Registros.All(registro => registro.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Correcto)));
        });
    }

    [Test]
    public void PartiallyCorrectResponse_Deserialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            LROEPJ240FacturasEmitidasConSGAltaRespuesta response = XmlSerializationHelper.
            Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(PartiallyCorrectResponseFilename);

            Assert.IsNotNull(response);
            Assert.True(response.Registros.Length == NumberOfRecords);

            //first registro is correct
            var firstRecord = response.Registros.First();
            Assert.True(firstRecord.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Correcto));

            //second registro is incorrect
            var secondRecord = response.Registros.Last();
            Assert.True(secondRecord.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Incorrecto));
        });
    }

    [Test]
    public void IncorrectResponse_Deserialization_Succeeds()
    {
        Assert.DoesNotThrow(() =>
        {
            LROEPJ240FacturasEmitidasConSGAltaRespuesta response = XmlSerializationHelper.
            Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(IncorrectResponseFilename);

            Assert.IsNotNull(response);
            Assert.True(response.Registros.Length == NumberOfRecords);
            Assert.True(response.Registros.All(registro => registro.SituacionRegistro.EstadoRegistro.Equals(EstadoRegistroEnum.Incorrecto)));
        });
    }

    [Test]
    public void WrongFileFormat_Deserialization_Fails()
    {
        Assert.Throws<InvalidOperationException>(() =>
        {
            XmlSerializationHelper.
                Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(TicketBaiFilename);
        });
    }


}
