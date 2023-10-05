﻿using Mews.Fiscalizations.Core.Xml;
using System.Text;

namespace Mews.Fiscalizations.Basque.Tests
{
    [TestFixture]
    public class BatuzInvoiceRequestDtoValidationTests
    {
        private const string BatuzXsdFilename = "./Xsd/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd";
        private const string BatuzTiposComplejosFilename = "./Xsd/batuz_TiposComplejos.xsd";
        private const string BatuzEnumeradosFilename = "./Xsd/batuz_Enumerados.xsd";
        private const string BatuzTiposBasicosFilename = "./Xsd/batuz_TiposBasicos.xsd";

        [Test]
        public void CreateTBatuzInvoiceDto_XmlSerialization_Succeeds()
        {
            var batuzInvoiceRequest = BatuzInvoiceRequestHelper.CreateSampleBatuzRequest();

            Assert.DoesNotThrow(() => 
            {
                var xmlElement = XmlSerializer.Serialize(batuzInvoiceRequest, new XmlSerializationParameters(
                    encoding: Encoding.UTF8,
                    namespaces: Array.Empty<XmlNamespace>()));
                Assert.NotNull(xmlElement);
            });

        }

        [Test]
        public void CreateTicketBaiInvoice_XsdValidation_Succeeds()
        {
            var batuzInvoiceRequest = BatuzInvoiceRequestHelper.CreateSampleBatuzRequest();
            var schemas = new Dictionary<string, string>
            {
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd", BatuzXsdFilename },
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd", BatuzTiposComplejosFilename },
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd", BatuzEnumeradosFilename },
                {"https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposBasicos.xsd", BatuzTiposBasicosFilename }
            };

            Assert.DoesNotThrow(() =>
            {
                var xmlElement = XmlSerializer.Serialize(batuzInvoiceRequest, new XmlSerializationParameters(
                    encoding: Encoding.UTF8,
                    namespaces: Array.Empty<XmlNamespace>()));
                XmlSchemaHelper.RunXmlSchemaValidation(element: xmlElement,
                validatingXsdFilename: BatuzXsdFilename, schemas);
            });
            
        }

    }
}
