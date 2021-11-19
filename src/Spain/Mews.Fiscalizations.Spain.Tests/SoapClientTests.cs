using Mews.Fiscalizations.Spain.Communication;
using Mews.Fiscalizations.Spain.Dto.Responses;
using Mews.Fiscalizations.Spain.Tests.IssuedInvoices;
using NUnit.Framework;
using System;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Tests
{
    [TestFixture]
    public class SoapClientTests
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

        [System.SerializableAttribute]
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
            Assert.IsFalse(soapFault.IsSuccess);
            throw new Exception($"{soapFault.ErrorResult.Code} >> {soapFault.ErrorResult.Message}");

        }
    }
}
