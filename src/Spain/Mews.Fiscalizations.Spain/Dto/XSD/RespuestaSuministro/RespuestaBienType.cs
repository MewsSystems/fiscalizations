using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class RespuestaBienType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public RespuestaBienTypePeriodoLiquidacion PeriodoLiquidacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public IDFacturaComunitariaType IDFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public string IdentificacionBien { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string RefExterna { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public EstadoRegistroType EstadoRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer", Order = 5)]
    public string CodigoErrorRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string DescripcionErrorRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public string CSV { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public RegistroDuplicadoType RegistroDuplicado { get; set; }
}