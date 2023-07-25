using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class RespuestaRecibidaPagoType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public IDFacturaRecibidaNombreBCType IDFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public EstadoRegistroType EstadoRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer", Order = 2)]
    public string CodigoErrorRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string DescripcionErrorRegistro { get; set; }
}