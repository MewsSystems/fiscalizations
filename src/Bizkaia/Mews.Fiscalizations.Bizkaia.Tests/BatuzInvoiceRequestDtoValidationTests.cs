using FuncSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    [TestFixture]
    internal class BatuzInvoiceRequestDtoValidationTests
    {
        private const string BatuzRequestFilename = "batuzRequest.xml";
        private const string BatuzXsdFilename = @"..\..\..\Xsd\LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd";
        private const string BatuzTiposComplejosFilename = @"..\..\..\Xsd\batuz_TiposComplejos.xsd";
        private const string BatuzEnumeradosFilename = @"..\..\..\Xsd\batuz_Enumerados.xsd";
        private const string BatuzTiposBasicosFilename = @"..\..\..\Xsd\batuz_TiposBasicos.xsd";

        [Test]
        public void CreateTBatuzInvoiceDto_XmlSerialization_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var batuzInvoiceRequest = BatuzInvoiceRequestHelper.CreateSampleBatuzRequest();

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(batuzInvoiceRequest, BatuzRequestFilename);

            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);
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
            bool serializationSucceeds = XmlSerializationSucceeds(batuzInvoiceRequest, BatuzRequestFilename);
            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);

            bool xsdValidationSucceeds = XmlSchemaHelper.XmlSchemaValidationSucceeds(xmlFilenameToValidate: BatuzRequestFilename,
                validatingXsdFilename: BatuzXsdFilename, schemas);
            Assert.True(xsdValidationSucceeds);

        }

        private bool XmlSerializationSucceeds(LROEPJ240FacturasEmitidasConSGAltaPeticion batuzInvoiceRequest, string batuzRequestFilename)
        {
            try
            {
                XmlSerializationHelper<LROEPJ240FacturasEmitidasConSGAltaPeticion>.SerializeToFile(batuzInvoiceRequest, batuzRequestFilename, Array.Empty<string>());
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
