using FuncSharp;
using System.Xml;

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

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(ticketBai, out XmlElement? xmlElement);

            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);
        }

        [Test]
        public void CreateTicketBaiInvoice_XsdValidation_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var ticketBai = TicketBaiInvoiceHelper.CreateSampleTbaiInvoice();

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(ticketBai, out XmlElement? xmlElement);
            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);
            Assert.NotNull(xmlElement);

            var schemas = new Dictionary<string, string>
            {
                {TicketBaiNamespace, TicketBaiXsdFilename },
                {SignatureNamespace, SignatureXsdFilename }
            };

            bool xsdValidationSucceeds = XmlSchemaHelper.XmlSchemaValidationSucceeds(element: xmlElement, 
                validatingXsdFilename: TicketBaiXsdFilename, schemasDictionary: schemas);
            Assert.True(xsdValidationSucceeds);

        }

        private bool XmlSerializationSucceeds(TicketBai ticketBai, out XmlElement? xmlElement)
        {
            try
            {
                xmlElement = XmlSerializationHelper<TicketBai>.Serialize(ticketBai, ReadOnlyList.Create("http://www.w3.org/2000/09/xmldsig#"));
                return true;
            } catch 
            {
                xmlElement = null;
                return false;
            }
        }
    }
}
