using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Invoice
{
    [Serializable, XmlType(Namespace = ElectronicInvoice.Namespace)]
    public class InvoiceAttachment
    {
        [XmlElement("NomeAttachment", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Name { get; set; }

        /// <summary>
        /// Required if the attachment is compressed. (ZIP, RAR, ...)
        /// </summary>
        [XmlElement("AlgoritmoCompressione", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string CompressionAlgorithm { get; set; }

        /// <summary>
        /// (PDF, XML, ...)
        /// </summary>
        [XmlElement("FormatoAttachment", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Format { get; set; }

        [XmlElement("DescrizioneAttachment", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string Description { get; set; }

        [XmlElement("Attachment", Form = XmlSchemaForm.Unqualified, DataType = "base64Binary")]
        public byte[] Content { get; set; }
    }
}