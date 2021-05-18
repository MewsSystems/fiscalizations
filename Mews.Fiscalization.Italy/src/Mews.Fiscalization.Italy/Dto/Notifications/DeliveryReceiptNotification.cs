using System;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlType(Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0")]
    [XmlRoot("RicevutaConsegna", Namespace = "http://www.fatturapa.gov.it/sdi/messaggi/v1.0", IsNullable = false)]
    public class DeliveryReceiptNotification : SdiNotification
    {
        public DeliveryReceiptNotification()
        {
            IntermediarioConDupliceRuolo = "Si";
        }

        [XmlElement("DataOraRicezione", Form = XmlSchemaForm.Unqualified)]
        public DateTime Received { get; set; }

        [XmlElement("DataOraConsegna", Form = XmlSchemaForm.Unqualified)]
        public DateTime Delivered { get; set; }

        [XmlElement("Destinatario", Form = XmlSchemaForm.Unqualified)]
        public Destination Destination { get; set; }

        [XmlElement("RiferimentoArchivio", Form = XmlSchemaForm.Unqualified)]
        public InvoiceArchive InvoiceArchive { get; set; }

        [XmlElement("PecMessageId", Form = XmlSchemaForm.Unqualified)]
        public string CemMessageId { get; set; }

        [XmlAttribute("IntermediarioConDupliceRuolo")]
        public string IntermediarioConDupliceRuolo { get; set; }
    }
}