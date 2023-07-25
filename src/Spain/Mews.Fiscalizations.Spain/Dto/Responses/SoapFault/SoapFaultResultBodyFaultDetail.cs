using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Dto.Responses.SoapFault;

[XmlType(AnonymousType = true)]
[XmlRoot(Namespace = "", ElementName = "detail", IsNullable = false)]
public class SoapFaultResultBodyFaultDetail
{
    [XmlElement(ElementName = "callstack")]
    public string CallStack { get; set; }
}