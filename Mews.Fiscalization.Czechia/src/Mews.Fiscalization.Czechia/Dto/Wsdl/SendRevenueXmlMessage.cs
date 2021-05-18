using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [XmlRoot(ElementName = "Trzba", Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class SendRevenueXmlMessage
    {
        [XmlElement(Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 0, ElementName = "Hlavicka")]
        public RevenueHeader Header { get; set; }

        [XmlElement(Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 1)]
        public RevenueData Data { get; set; }

        [XmlElement(Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 2, ElementName = "KontrolniKody")]
        public RevenueSecurityCode SecurityCode { get; set; }
    }
}
