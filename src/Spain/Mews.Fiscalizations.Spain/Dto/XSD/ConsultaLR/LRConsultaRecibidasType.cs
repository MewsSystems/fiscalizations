using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
public class LRConsultaRecibidasType : ConsultaInformacion
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public LRFiltroRecibidasType FiltroConsulta { get; set; }
}