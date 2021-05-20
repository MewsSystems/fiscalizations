using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    public class InvoiceReference
    {
        [XmlElement("NumeroFattura", Form = XmlSchemaForm.Unqualified, DataType = "normalizedString")]
        public string InvoiceNumber { get; set; }

        [XmlElement("AnnoFattura", Form = XmlSchemaForm.Unqualified, DataType = "nonNegativeInteger")]
        public string InvoiceYear { get; set; }

        [XmlElement("PosizioneFattura", Form = XmlSchemaForm.Unqualified, DataType = "positiveInteger")]
        public string PosizioneFattura { get; set; }
    }
}