using System.Xml.Serialization;
using Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

namespace Mews.Fiscalizations.Spain.Dto.Responses
{
    [System.SerializableAttribute]
    [XmlRoot(ElementName = "RespuestaLRBajaFacturasEmitidas", Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
    [XmlType(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
    public class RemoveIssuedInvoicesResponse : RespuestaComunBajaType
    {
        [XmlElement("RespuestaLinea", Order=0)]
        public RespuestaExpedidaBajaType[] RespuestaLinea { get; set; }
    }
}