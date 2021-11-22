using System;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Mews.Fiscalizations.Spain.Communication;
using Mews.Fiscalizations.Spain.Dto.Responses;
using Mews.Fiscalizations.Spain.Tests.IssuedInvoices;
using NUnit.Framework;
using FuncSharp;

namespace Mews.Fiscalizations.Spain.Tests
{
    // TODO: remove tests when feature will be completed
    [TestFixture]
    public class SoapFaultResponseDeserializationTests
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
            Assert.AreEqual(soapFault.ErrorResult.Get().Code, "env:Client");
            var message = soapFault.ErrorResult.Map(e => e.Message);
            Assert.IsTrue(message.Map(m => m.Contains("El XML no cumple el esquema.")).GetOrFalse(), $"Expected fragment not found in message {message.GetOrNull()}");
        }
    }
}
