using System;
using System.Collections.Generic;
using System.Security;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Bizkaia.Tests;


[TestFixture]
public class BatuzInvoiceResponseDeserializationTests
{
    private const string CorrectResponseFilename = @"..\..\..\Documents\LROE_240_FacturasEmitidasConSG_Correcta.xml";
    private const string PartiallyCorrectResponseFilename = @"..\..\..\Documents\LROE_240_FacturasEmitidasConSG_Parc_Correcta.xml";
    private const string IncorrectResponseFilename = @"..\..\..\Documents\LROE_240_FacturasEmitidasConSG_Incorrecta.xml";
    private const string TicketBaiFilename = @"..\..\..\Documents\ticketBai.xml";
    private const string OkStatus = "Correcto";
    private const string FailedStatus = "Incorrecto";
    private const int NumberOfRecords = 2;

    [Test]
    public void CorrectResponse_Deserialization_Succeeds()
    {
        LROEPJ240FacturasEmitidasConSGAltaRespuesta response = XmlSerializationHelper<LROEPJ240FacturasEmitidasConSGAltaRespuesta>.
            Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(CorrectResponseFilename);

        Assert.IsNotNull(response);
        Assert.True(response.Registros.Length == NumberOfRecords);

        Assert.True(response.Registros.All(registro => registro.SituacionRegistro.EstadoRegistro.Equals(OkStatus)));
    }

    [Test]
    public void PartiallyCorrectResponse_Deserialization_Succeeds()
    {
        LROEPJ240FacturasEmitidasConSGAltaRespuesta response = XmlSerializationHelper<LROEPJ240FacturasEmitidasConSGAltaRespuesta>.
            Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(PartiallyCorrectResponseFilename);

        Assert.IsNotNull(response);
        Assert.True(response.Registros.Length == NumberOfRecords);

        //first registro is correct
        var firstRecord = response.Registros.First();
        Assert.True(firstRecord.SituacionRegistro.EstadoRegistro.Equals(OkStatus));

        //second registro is incorrect
        var secondRecord = response.Registros.Last();
        Assert.True(secondRecord.SituacionRegistro.EstadoRegistro.Equals(FailedStatus));
    }

    [Test]
    public void IncorrectResponse_Deserialization_Succeeds()
    {
        LROEPJ240FacturasEmitidasConSGAltaRespuesta response = XmlSerializationHelper<LROEPJ240FacturasEmitidasConSGAltaRespuesta>.
            Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(IncorrectResponseFilename);

        Assert.IsNotNull(response);
        Assert.True(response.Registros.Length == NumberOfRecords);
        Assert.True(response.Registros.All(registro => registro.SituacionRegistro.EstadoRegistro.Equals(FailedStatus)));
    }

    [Test]
    public void WrongFileFormat_Deserialization_Fails()
    {
        Assert.Throws<InvalidOperationException>(InvalidFileformatDeserialization);
    }

    private void InvalidFileformatDeserialization()
    {
        XmlSerializationHelper<LROEPJ240FacturasEmitidasConSGAltaRespuesta>.
                Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(TicketBaiFilename);
    }
}
