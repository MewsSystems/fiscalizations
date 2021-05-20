using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    [XmlRoot("NotificaDecorrenzaTermini", Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0", IsNullable = false)]
    public class DeadlinePassedNotification : SdiNotification
    {
        public DeadlinePassedNotification()
        {
            IntermediarioConDupliceRuolo = "Si";
        }

        [XmlElement("RiferimentoFattura", Form = XmlSchemaForm.Unqualified)]
        public InvoiceReference InvoiceReference { get; set; }

        [XmlElement("Descrizione", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        [XmlElement("PecMessageId", Form = XmlSchemaForm.Unqualified)]
        public string CemMessageId { get; set; }

        [XmlAttribute("IntermediarioConDupliceRuolo")]
        public string IntermediarioConDupliceRuolo { get; set; }
    }
}