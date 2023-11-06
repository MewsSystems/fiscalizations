namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class NoSujetaType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string ImportePorArticulos7_14_Otros { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string ImporteTAIReglasLocalizacion { get; set; }
}