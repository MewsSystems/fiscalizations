namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class DesgloseRectificacionType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string BaseRectificada { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string CuotaRectificada { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string CuotaRecargoRectificado { get; set; }
}