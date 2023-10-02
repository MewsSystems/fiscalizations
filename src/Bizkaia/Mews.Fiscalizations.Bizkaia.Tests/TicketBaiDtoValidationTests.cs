using FuncSharp;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    [TestFixture]
    public class TicketBaiDtoValidationTests
    {
        private const string TicketBaiInvoiceFilename = @"C:\Users\InakiEsteve\Documents\Batuz\Ejemplos\ticketBAI_decoded_ok.xml"; //"ticketBai.xml";
        private const string TicketBaiXsdFilename = @"..\..\..\Xsd\ticketBaiV1-2-1.xsd";

        [Test]
        public void CreateTicketBaiDto_XmlSerialization_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var ticketBai = TicketBaiInvoiceHelper.CreateSampleTbaiInvoice();

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(ticketBai, TicketBaiInvoiceFilename);

            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);
        }

        [Test]
        public void CreateTicketBaiInvoice_XsdValidation_Succeeds()
        {
            //Arrange create a dto that matches the samples provided by the Bizkaia authorities 
            var ticketBai = TicketBaiInvoiceHelper.CreateSampleTbaiInvoice();

            //act check that xml serialization of the ticketBai succeeds without errors
            bool serializationSucceeds = XmlSerializationSucceeds(ticketBai, TicketBaiInvoiceFilename);
            //assert that xml serialization was possible
            Assert.True(serializationSucceeds);

            bool xsdValidationSucceeds = XmlSchemaHelper.XmlSchemaValidationSucceeds(xmlFilenameToValidate: TicketBaiInvoiceFilename, 
                validatingXsd: TicketBaiXsdFilename);
            Assert.True(xsdValidationSucceeds);

        }

        private bool XmlSerializationSucceeds(TicketBai ticketBai, string ticketBaiFilename)
        {
            try
            {
                XmlSerializationHelper<TicketBai>.SerializeToFile(ticketBai, TicketBaiInvoiceFilename, ReadOnlyList.Create("http://www.w3.org/2000/09/xmldsig#"));
                return true;
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
