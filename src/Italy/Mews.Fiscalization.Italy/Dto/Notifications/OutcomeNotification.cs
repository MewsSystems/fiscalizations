using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    [XmlRoot("NotificaEsito", Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0", IsNullable = false)]
    public class OutcomeNotification : SdiNotification
    {
        public OutcomeNotification()
        {
            IntermediarioConDupliceRuolo = "Si";
        }

        [XmlElement("EsitoCommittente", Form = XmlSchemaForm.Unqualified)]
        public ClientOutcomeNotification ClientOutcomeNotification { get; set; }

        [XmlElement("PecMessageId", Form = XmlSchemaForm.Unqualified)]
        public string CemMessageId { get; set; }

        [XmlAttribute("IntermediarioConDupliceRuolo")]
        public string IntermediarioConDupliceRuolo { get; set; }
    }
}