using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Dto.Responses.SoapFault
{
    [XmlType(AnonymousType = true, Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]
    [XmlRoot(Namespace = "http://schemas.xmlsoap.org/soap/envelope/", ElementName = "Envelope", IsNullable = false)]
    public partial class SoapFaultResponse
    {
        public SoapFaultResultBody Body { get; set; }
    }
}
