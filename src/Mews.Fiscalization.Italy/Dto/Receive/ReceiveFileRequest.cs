using System;
using System.Xml.Serialization;

namespace Mews.Fiscalization.Italy.Dto.Receive
{
    [Serializable]
    [XmlRoot("fileSdIAccoglienza", Namespace = "http://www.fatturapa.gov.it/sdi/ws/trasmissione/v1.0/types")]
    public class ReceiveFile
    {
        [XmlElement("NomeFile", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, Order = 0)]
        public string FileName { get; set; }

        [XmlElement("File", Form = System.Xml.Schema.XmlSchemaForm.Unqualified, DataType = "base64Binary", Order = 1)]
        public byte[] Content { get; set; }
    }
}
