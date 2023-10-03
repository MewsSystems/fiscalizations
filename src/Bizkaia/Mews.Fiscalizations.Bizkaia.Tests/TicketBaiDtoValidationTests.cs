using FuncSharp;
using Mews.Fiscalizations.Core.Xml;
using System.Text;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    [TestFixture]
    public class TicketBaiDtoValidationTests
    {
        private const string TicketBaiXsdFilename = @"./Xsd/ticketBaiV1-2-1.xsd";
        private const string SignatureXsdFilename = @"./Xsd/xmldsig-core-schema.xsd";
        private const string TicketBaiNamespace = "urn:ticketbai:emision";
        private const string SignatureNamespace = "http://www.w3.org/2000/09/xmldsig#";

        [Test]
        public void CreateTicketBaiDto_XmlSerialization_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var ticketBai = TicketBaiInvoiceHelper.CreateSampleTbaiInvoice();

            Assert.DoesNotThrow(() => 
            {
                var xmlElement = XmlSerializer.Serialize(
                    ticketBai, 
                    new XmlSerializationParameters(
                        encoding: Encoding.UTF8,
                        namespaces: ReadOnlyList.Create(new XmlNamespace("http://www.w3.org/2000/09/xmldsig#"))
                        )
                    );
                Assert.IsNotNull(xmlElement);
            });
        }

        [Test]
        public void CreateTicketBaiInvoice_XsdValidation_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var ticketBai = TicketBaiInvoiceHelper.CreateSampleTbaiInvoice();

            var schemas = new Dictionary<string, string>
            {
                {TicketBaiNamespace, TicketBaiXsdFilename },
                {SignatureNamespace, SignatureXsdFilename }
            };

            Assert.DoesNotThrow(() =>
            {
                var xmlElement = XmlSerializer.Serialize(
                    ticketBai,
                    new XmlSerializationParameters(
                        encoding: Encoding.UTF8,
                        namespaces: ReadOnlyList.Create(new XmlNamespace("http://www.w3.org/2000/09/xmldsig#"))
                        )
                    );
                Assert.IsNotNull(xmlElement);
                XmlSchemaHelper.RunXmlSchemaValidation(
                    element: xmlElement,
                    validatingXsdFilename: TicketBaiXsdFilename, 
                    schemasDictionary: schemas
                );
            });
        
        }

    }
}
