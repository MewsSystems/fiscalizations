using System.Xml.Schema;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    [XmlRoot("attestazioneTrasmissioneFattura", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class SdiNotificationMessage
    {
        [XmlElement("IdentificativoSdI", Form = XmlSchemaForm.Unqualified)]
        public string SdiIdentification { get; set; }

        [XmlElement("NomeFile", Form = XmlSchemaForm.Unqualified)]
        public string FileName { get; set; }

        [XmlElement("File", Form = XmlSchemaForm.Unqualified)]
        public byte[] FileContent { get; set; }
    }

    [XmlRoot("attestazioneTrasmissioneFattura", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class DeliveryReceiptNotificationMessage : SdiNotificationMessage
    {
    }

    [XmlRoot("notificaMancataConsegna", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class FailedDeliveryNotificationMessage : SdiNotificationMessage
    {
    }

    [XmlRoot("notificaScarto", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class RejectionNotificationMessage : SdiNotificationMessage
    {
    }

    [XmlRoot("notificaEsito", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class OutcomeNotificationMessage : SdiNotificationMessage
    {
    }

    [XmlRoot("notificaDecorrenzaTermini", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class DeadlinePassedNotificationMessage : SdiNotificationMessage
    {
    }

    [XmlRoot("attestazioneTrasmissioneFattura", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class ImpossibleDeliveryNotificationMessage : SdiNotificationMessage
    {
    }
}
