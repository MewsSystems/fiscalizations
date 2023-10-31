namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class FacturaRecibidaType : FacturaType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public DesgloseFacturaRecibidasType DesgloseFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public PersonaFisicaJuridicaType Contraparte { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string FechaRegContable { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string CuotaDeducible { get; set; }
}