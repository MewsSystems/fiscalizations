using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFactInformadasAgrupadasClienteType))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaLRFacturasAgrupadasClienteType : ConsultaInformacionCliente
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public ResultadoConsultaType ResultadoConsulta { get; set; }
    }
}