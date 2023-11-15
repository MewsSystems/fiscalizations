namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class RespuestaLROComunitariasType : RespuestaComunAltaType
{
    [System.Xml.Serialization.XmlElementAttribute("RespuestaLinea", Order = 0)]
    public RespuestaComunitariaType[] RespuestaLinea { get; set; }
}