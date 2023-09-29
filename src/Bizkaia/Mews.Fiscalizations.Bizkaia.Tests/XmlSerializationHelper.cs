using FuncSharp;
using Mews.Fiscalizations.Core.Xml;
using System.Text;
using System.Xml;
using XmlSerializer = Mews.Fiscalizations.Core.Xml.XmlSerializer;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    public static class XmlSerializationHelper
    {
        public static void SerializeToFile(TicketBai ticketBai, string filename)
        {
            var xmlElement = XmlSerializer.Serialize(ticketBai, new XmlSerializationParameters(
            encoding: Encoding.UTF8,
            namespaces: NonEmptyEnumerable.Create(
                new XmlNamespace("http://www.w3.org/2000/09/xmldsig#")
                )
            ));

            var document = new XmlDocument();
            document.LoadXml(xmlElement.OuterXml);
            document.Save(filename);
        }
    }
}
