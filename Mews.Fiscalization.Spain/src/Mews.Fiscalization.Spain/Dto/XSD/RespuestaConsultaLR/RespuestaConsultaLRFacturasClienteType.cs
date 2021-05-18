using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaLRFactInformadasClienteType))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaLRFacturasClienteType : ConsultaInformacionCliente
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IndicadorPaginacionType IndicadorPaginacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ResultadoConsultaType ResultadoConsulta { get; set; }
    }
}