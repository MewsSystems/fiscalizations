using Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR;
using Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaFacturaPagosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaPagosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaInmueblesType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaInmueblesAdicionalesType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaFacturaCobrosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaCobrosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLROperacionesSegurosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRAgenciasViajesType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRCobrosMetalicoType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRDetOperIntracomunitariasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRBienesInversionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasRecibidasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasEmitidasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConsultaInmueblesAdicionalesType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConsultaPagosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConsultaCobrosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaLROperacionesSegurosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaAgenciasViajesType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaCobrosMetalicoType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaDetOperIntracomunitariasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaBienesInversionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaEmitidasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRConsultaRecibidasType))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class ConsultaInformacion
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public CabeceraConsultaSii Cabecera { get; set; }
    }
}