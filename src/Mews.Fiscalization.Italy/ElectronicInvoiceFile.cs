using System.Text;
using System.Xml.Serialization;
using Mews.Fiscalization.Italy.Dto.Invoice;
using Mews.Fiscalization.Italy.Utils;

namespace Mews.Fiscalization.Italy
{
    public class ElectronicInvoiceFile
    {
        private const string XmlFileHeader = @"<?xml version=""1.0"" encoding=""utf-8""?>";

        public byte[] Data { get; }

        public string FileName { get; }

        public string Content
        {
            get { return Encoding.UTF8.GetString(Data); }
        }

        public ElectronicInvoiceFile(ElectronicInvoice invoice)
        {
            Data = Encoding.UTF8.GetBytes(Serialize(invoice));
            FileName = FileUtils.SanitizePath($"{invoice.Header.TransmissionData.SequentialNumber}.xml");
        }

        private string Serialize(ElectronicInvoice invoice)
        {
            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add("p", ElectronicInvoice.Namespace);

            var xml = XmlManipulator.Serialize(invoice, xmlNamespaces).OuterXml;
            return $@"{XmlFileHeader}{xml}";
        }
    }
}
