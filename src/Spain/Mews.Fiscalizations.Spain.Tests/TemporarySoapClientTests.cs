using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Mews.Fiscalizations.Spain.Communication;
using Mews.Fiscalizations.Spain.Dto.Responses;
using Mews.Fiscalizations.Spain.Tests.IssuedInvoices;
using NUnit.Framework;

namespace Mews.Fiscalizations.Spain.Tests
{
    // TODO: remove tests when feature will be completed
    [TestFixture]
    public class TemporarySoapClientTests
    {
        private SoapClient soapClient;

        [OneTimeSetUp]
        public void SetUp()
        {
            soapClient = new SoapClient(
                endpointUri: new Uri("https://www7.aeat.es/wlpl/SSII-FACT/ws/fe/SiiFactFEV1SOAP"),
                certificate: Basics.Certificate,
                httpTimeout: TimeSpan.FromSeconds(30)
            );
        }

        [XmlRoot(ElementName = "SuministroLRFacturasEmitidas", Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
        [XmlType(TypeName = "SuministroLRFacturasEmitidas", AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
        public class InvalidSoapMessage
        {
            [XmlElement("Message")]
            public string Message { get; set; }
        }

        [XmlRoot("message")]
        public class SomeSoapResponse
        {
            [XmlAttribute("value")]
            public string Value { get; set; }
        }

        [Test]
        public async Task InvalidSoapActionShouldFail()
        {
            var soapFault = await soapClient.SendAsync<InvalidSoapMessage, SubmitIssuedInvoicesResponse>(new InvalidSoapMessage { Message = "bla-bla-bla" });
            Assert.IsFalse(soapFault.IsSuccess, "Soap fail was expected but success received");
            Assert.IsTrue(string.IsNullOrEmpty(soapFault?.ErrorResult?.Code), "Non-empty code was expected");
            Assert.IsTrue(string.IsNullOrEmpty(soapFault?.ErrorResult?.Message), "Non-empty message was expected");
        }
    }
}
