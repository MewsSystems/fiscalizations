using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlTypeAttribute(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class ResponseHeader
    {
        [XmlAttribute(AttributeName = "uuid_zpravy")]
        public string MessageUuid { get; set; }

        [XmlAttribute(AttributeName = "bkp")]
        public string SecurityCode { get; set; }

        [XmlAttribute(AttributeName = "dat_prij")]
        public DateTime Accepted { get; set; }

        [XmlIgnore]
        public bool AcceptedSpecified { get; set; }

        [XmlAttribute(AttributeName = "dat_odmit")]
        public DateTime Rejected { get; set; }

        [XmlIgnore]
        public bool RejectedSpecified { get; set; }
    }
}
