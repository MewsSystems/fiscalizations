namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class DetalleIVARecibida2Type
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string TipoImpositivo { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public string BaseImponible { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string CuotaSoportada { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string TipoRecargoEquivalencia { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public string CuotaRecargoEquivalencia { get; set; }
}