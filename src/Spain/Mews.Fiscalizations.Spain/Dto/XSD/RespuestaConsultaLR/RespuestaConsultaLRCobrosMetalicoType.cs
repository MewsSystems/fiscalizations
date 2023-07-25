namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public class RespuestaConsultaLRCobrosMetalicoType : RespuestaConsultaLRFacturasType
{
    [System.Xml.Serialization.XmlElementAttribute("RegistroRespuestaConsultaLRCobrosMetalico", Order = 0)]
    public RegistroRespuestaConsultaCobrosMetalicoType[] RegistroRespuestaConsultaLRCobrosMetalico { get; set; }
}