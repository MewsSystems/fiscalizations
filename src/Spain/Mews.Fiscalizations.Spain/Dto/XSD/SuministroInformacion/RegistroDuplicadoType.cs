namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class RegistroDuplicadoType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public EstadoRegistroSIIType EstadoRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer", Order = 1)]
    public string CodigoErrorRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string DescripcionErrorRegistro { get; set; }
}