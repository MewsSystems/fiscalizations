namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class IDFacturaExpedidaTypeIDEmisorFactura
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string NIF { get; set; }
}