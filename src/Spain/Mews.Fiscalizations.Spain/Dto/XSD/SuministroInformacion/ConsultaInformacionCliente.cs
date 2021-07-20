using Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR;
using Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasAgrupadasClienteType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFactInformadasAgrupadasClienteType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFacturasClienteType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFactInformadasClienteType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConsultaLRFactInformadasAgrupadasClienteType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(ConsultaLRFactInformadasClienteType))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class ConsultaInformacionCliente
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public CabeceraConsultaSiiCliente Cabecera { get; set; }
    }
}