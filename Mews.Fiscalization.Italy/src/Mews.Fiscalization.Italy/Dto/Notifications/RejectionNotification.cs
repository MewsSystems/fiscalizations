using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    [XmlRoot("NotificaScarto", Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0", IsNullable = false)]
    public class RejectionNotification : SdiNotification
    {
        [XmlElement("DataOraRicezione", Form = XmlSchemaForm.Unqualified)]
        public DateTime Received { get; set; }

        [XmlElement("RiferimentoArchivio", Form = XmlSchemaForm.Unqualified)]
        public InvoiceArchive InvoiceArchive { get; set; }

        [XmlArray(Form = XmlSchemaForm.Unqualified)]
        [XmlArrayItem("Errore", Form = XmlSchemaForm.Unqualified, IsNullable = false)]
        public SdiError[] Errors { get; set; }

        [XmlElement("PecMessageId", Form = XmlSchemaForm.Unqualified)]
        public string CemMessageId { get; set; }
    }
}