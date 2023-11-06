using System.Xml.Serialization;
using Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

namespace Mews.Fiscalizations.Spain.Dto.Responses;

[Serializable]
[XmlRoot(ElementName = "RespuestaLRFacturasEmitidas", Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
[XmlType(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class SubmitIssuedInvoicesResponse : RespuestaComunAltaType
{
    [XmlElement("RespuestaLinea", Order = 0)]
    public RespuestaExpedidaType[] RespuestaLinea { get; set; }
}