using System.Xml.Schema;
using System.Xml.Serialization;
using Mews.Fiscalization.Italy.Dto.XmlSignature;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    public class ClientOutcomeNotification
    {
        public ClientOutcomeNotification()
        {
            Version = "1.0";
        }

        [XmlElement("IdentificativoSdI", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string SdiIdentification { get; set; }

        [XmlElement("RiferimentoFattura", Form = XmlSchemaForm.Unqualified)]
        public InvoiceReference InvoiceReference { get; set; }

        [XmlElement("Esito", Form = XmlSchemaForm.Unqualified)]
        public ClientOutcome ClientOutcome { get; set; }

        [XmlElement("Descrizione", Form = XmlSchemaForm.Unqualified)]
        public string Description { get; set; }

        [XmlElement("MessageIdCommittente", Form = XmlSchemaForm.Unqualified)]
        public string ClientMessageId { get; set; }

        [XmlAttribute("versione")]
        public string Version { get; set; }

        [XmlElement("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }
}