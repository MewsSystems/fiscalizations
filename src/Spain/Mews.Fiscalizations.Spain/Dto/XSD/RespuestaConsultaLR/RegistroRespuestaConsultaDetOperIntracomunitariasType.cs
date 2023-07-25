using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public class RegistroRespuestaConsultaDetOperIntracomunitariasType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public IDFacturaComunitariaType IDFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public RespuestaDetOperIntracomunitariaType DatosDetOperIntracomunitarias { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public DatosPresentacion2Type DatosPresentacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public EstadoFactura2Type EstadoFactura { get; set; }
}