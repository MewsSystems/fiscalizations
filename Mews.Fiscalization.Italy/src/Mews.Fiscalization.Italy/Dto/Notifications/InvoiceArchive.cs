using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    public class InvoiceArchive
    {
        [XmlElement("IdentificativoSdI", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string SdiIdentification { get; set; }

        [XmlElement("NomeFile", Form = XmlSchemaForm.Unqualified)]
        public string FileName { get; set; }
    }
}