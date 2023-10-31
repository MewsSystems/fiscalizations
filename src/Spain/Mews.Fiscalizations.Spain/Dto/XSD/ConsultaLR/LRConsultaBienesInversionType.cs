using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
public class LRConsultaBienesInversionType : ConsultaInformacion
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public LRFiltroBienInversionType FiltroConsulta { get; set; }
}