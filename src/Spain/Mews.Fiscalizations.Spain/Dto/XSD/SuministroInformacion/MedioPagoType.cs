namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public enum MedioPagoType
{
    [System.Xml.Serialization.XmlEnumAttribute("01")]
    Item01,
    [System.Xml.Serialization.XmlEnumAttribute("02")]
    Item02,
    [System.Xml.Serialization.XmlEnumAttribute("03")]
    Item03,
    [System.Xml.Serialization.XmlEnumAttribute("04")]
    Item04,
    [System.Xml.Serialization.XmlEnumAttribute("05")]
    Item05,
}