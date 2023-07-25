namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class FacturaExpedidaTypeTipoDesglose
{
    public FacturaExpedidaTypeTipoDesglose()
    {
    }
    public FacturaExpedidaTypeTipoDesglose(TipoSinDesgloseType item) // TODO - fix name.
        : this()
    {
        Item = item;
    }
    public FacturaExpedidaTypeTipoDesglose(TipoConDesgloseType item) // TODO - fix name.
        : this()
    {
        Item = item;
    }

    [System.Xml.Serialization.XmlElementAttribute("DesgloseFactura", typeof(TipoSinDesgloseType), Order = 0)]
    [System.Xml.Serialization.XmlElementAttribute("DesgloseTipoOperacion", typeof(TipoConDesgloseType), Order = 0)]
    public object Item { get; set; }
}