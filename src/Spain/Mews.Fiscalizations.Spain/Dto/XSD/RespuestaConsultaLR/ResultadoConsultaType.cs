namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public enum ResultadoConsultaType
{
    ConDatos,
    SinDatos,
}