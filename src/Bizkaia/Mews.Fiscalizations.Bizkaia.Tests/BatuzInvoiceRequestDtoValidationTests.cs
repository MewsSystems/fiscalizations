using FuncSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    [TestFixture]
    public class BatuzInvoiceRequestDtoValidationTests
    {
        private const string BatuzXsdFilename = @"./Xsd/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd";
        private const string BatuzTiposComplejosFilename = @"./Xsd/batuz_TiposComplejos.xsd";
        private const string BatuzEnumeradosFilename = @"./Xsd/batuz_Enumerados.xsd";
        private const string BatuzTiposBasicosFilename = @"./Xsd/batuz_TiposBasicos.xsd";

        [Test]
        public void CreateTBatuzInvoiceDto_XmlSerialization_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var batuzInvoiceRequest = BatuzInvoiceRequestHelper.CreateSampleBatuzRequest();

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(batuzInvoiceRequest, out XmlElement xmlElement);

            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);
            Assert.NotNull(xmlElement);
        }

        [Test]
        public void CreateTicketBaiInvoice_XsdValidation_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var batuzInvoiceRequest = BatuzInvoiceRequestHelper.CreateSampleBatuzRequest();
            var schemas = new Dictionary<string, string>
            {
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd", BatuzXsdFilename },
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd", BatuzTiposComplejosFilename },
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd", BatuzEnumeradosFilename },
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposBasicos.xsd", BatuzTiposBasicosFilename }
            };

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(batuzInvoiceRequest, out XmlElement xmlElement);
            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);

            bool xsdValidationSucceeds = XmlSchemaHelper.XmlSchemaValidationSucceeds(element: xmlElement,
                validatingXsdFilename: BatuzXsdFilename, schemas);
            Assert.True(xsdValidationSucceeds);

        }

        private bool XmlSerializationSucceeds(LROEPJ240FacturasEmitidasConSGAltaPeticion batuzInvoiceRequest, out XmlElement? xmlElement)
        {
            try
            {
                xmlElement = XmlSerializationHelper<LROEPJ240FacturasEmitidasConSGAltaPeticion>.Serialize(batuzInvoiceRequest, Array.Empty<string>());
                return true;
            }
            catch 
            {
                xmlElement = null;
                return false;
            }
        }
    }
}
