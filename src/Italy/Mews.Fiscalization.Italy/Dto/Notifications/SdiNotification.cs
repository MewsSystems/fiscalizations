using System.Xml.Schema;
using System.Xml.Serialization;
using Mews.Fiscalization.Italy.Dto.XmlSignature;

namespace Mews.Fiscalization.Italy.Dto.Notifications
{
    public class SdiNotification
    {
        public SdiNotification()
        {
            Version = "1.0";
        }

        [XmlElement("IdentificativoSdI", Form = XmlSchemaForm.Unqualified, DataType = "integer")]
        public string SdiIdentification { get; set; }

        [XmlElement("NomeFile", Form = XmlSchemaForm.Unqualified)]
        public string FileName { get; set; }

        [XmlElement("MessageId", Form = XmlSchemaForm.Unqualified)]
        public string MessageId { get; set; }

        [XmlAttribute("versione")]
        public string Version { get; set; }

        [XmlElement("Note", Form = XmlSchemaForm.Unqualified)]
        public string Note { get; set; }

        [XmlElement("Signature", Namespace = "http://www.w3.org/2000/09/xmldsig#")]
        public Signature Signature { get; set; }
    }
}
