using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaConsultaInmueblesAdicionalesType))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaInmueblesType : ConsultaInformacion
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IDFacturaExpedidaBCType IDFactura { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ResultadoConsultaType ResultadoConsulta { get; set; }
    }
}