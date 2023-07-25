using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Dto.Responses.SoapFault
{
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    public class SoapFaultResultBodyFault
    {
        [XmlElement(Namespace = "", ElementName = "faultcode")]
        public string Code { get; set; }

        [XmlElement(Namespace = "", ElementName = "faultstring")]
        public string Message { get; set; }

        [XmlElement(Namespace = "", ElementName = "detail")]
        public SoapFaultResultBodyFaultDetail Detail { get; set; }
    }
}
