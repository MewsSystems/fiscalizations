using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public class RegistroRespuestaConsultaRecibidasType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public IDFacturaRecibidaType IDFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public FacturaRespuestaRecibidaType DatosFacturaRecibida { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public DatosPresentacion2Type DatosPresentacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public EstadoFacturaType EstadoFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public DatosDescuadreContraparteType DatosDescuadreContraparte { get; set; }
}