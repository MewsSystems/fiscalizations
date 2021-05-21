using System.Xml.Serialization;

namespace Mews.Eet.Dto.Wsdl
{
    [XmlRoot(ElementName = "Odpoved", Namespace = "http://fs.mfcr.cz/eet/schema/v3")]
    public class SendRevenueXmlResponse
    {
        [XmlElement(Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 0, ElementName = "Hlavicka")]
        public ResponseHeader Header;

        [XmlElement(ElementName = "Chyba", Type = typeof(ResponseError), Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 1)]
        [XmlElement(ElementName = "Potvrzeni", Type = typeof(ResponseSuccess), Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 1)]
        public object Item;

        [XmlElement(ElementName = "Varovani", Namespace = "http://fs.mfcr.cz/eet/schema/v3", Order = 2)]
        public ResponseWarning[] Warning;
    }
}
