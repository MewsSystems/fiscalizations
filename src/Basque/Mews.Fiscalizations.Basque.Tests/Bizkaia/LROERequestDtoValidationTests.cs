﻿using Mews.Fiscalizations.Basque.Tests.Bizkaia.Helpers;
using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Basque.Tests.Bizkaia
{
    [TestFixture]
    public class LROERequestDtoValidationTests
    {
        private const string BatuzXsdFilename = "./Bizkaia/Xsd/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd";
        private const string BatuzTiposComplejosFilename = "./Bizkaia/Xsd/batuz_TiposComplejos.xsd";
        private const string BatuzEnumeradosFilename = "./Bizkaia/Xsd/batuz_Enumerados.xsd";
        private const string BatuzTiposBasicosFilename = "./Bizkaia/Xsd/batuz_TiposBasicos.xsd";

        [Test]
        public void CreateBatuzInvoiceDto_XmlSerialization_Succeeds()
        {
            var batuzInvoiceRequest = LROERequestHelper.CreateSampleBatuzRequest();

            Assert.DoesNotThrow(() =>
            {
                _ = XmlSerializer.Serialize(batuzInvoiceRequest);
            });

        }

        [Test]
        public void CreateTicketBaiInvoice_XsdValidation_Succeeds()
        {
            var batuzInvoiceRequest = LROERequestHelper.CreateSampleBatuzRequest();
            var schemas = new Dictionary<string, string>
            {
                { "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/LROE_PJ_240_1_1_FacturasEmitidas_ConSG_AltaPeticion_V1_0_2.xsd", BatuzXsdFilename },
                { "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposComplejos.xsd", BatuzTiposComplejosFilename },
                { "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_Enumerados.xsd", BatuzEnumeradosFilename },
                { "https://www.batuz.eus/fitxategiak/batuz/LROE/esquemas/batuz_TiposBasicos.xsd", BatuzTiposBasicosFilename }
            };

            Assert.DoesNotThrow(() =>
            {
                var xmlElement = XmlSerializer.Serialize(batuzInvoiceRequest);
                XmlSchemaHelper.RunXmlSchemaValidation(element: xmlElement,
                schemasDictionary: schemas);
            });

        }

    }
}
