using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLROperacionesSegurosType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRAgenciasViajesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRCobrosMetalicoType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRDetOperIntracomunitariasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRBienesInversionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasRecibidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasEmitidasType))]
[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public class RespuestaConsultaLRFacturasType : ConsultaInformacion
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public RespuestaConsultaLRFacturasTypePeriodoLiquidacion PeriodoLiquidacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public IndicadorPaginacionType IndicadorPaginacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public ResultadoConsultaType ResultadoConsulta { get; set; }
}