using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public class FacturaRespuestaExpedidaTypeTipoDesglose
{
    [System.Xml.Serialization.XmlElementAttribute("DesgloseFactura", typeof(TipoSinDesgloseType), Order = 0)]
    [System.Xml.Serialization.XmlElementAttribute("DesgloseTipoOperacion", typeof(TipoConDesgloseType), Order = 0)]
    public object Item { get; set; }
}