using System;
using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [Serializable]
    [XmlType(Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class RevenueSecurityCode
    {
        [XmlElement(Order = 0, ElementName = "pkp")]
        public Signature Signature { get; set; }

        [XmlElement(Order = 1, ElementName = "bkp")]
        public SecurityCode SecurityCode { get; set; }
    }
}
