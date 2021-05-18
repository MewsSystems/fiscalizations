using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    public class Destination
    {
        [XmlElement("Codice", Form = XmlSchemaForm.Unqualified)]
        public string Code { get; set; }

        [XmlElement("Descrizione", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }
    }
}