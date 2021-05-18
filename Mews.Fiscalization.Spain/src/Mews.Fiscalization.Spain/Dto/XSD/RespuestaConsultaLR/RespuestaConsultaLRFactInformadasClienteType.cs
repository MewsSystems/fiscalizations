namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaLRFactInformadasClienteType : RespuestaConsultaLRFacturasClienteType
    {
        [System.Xml.Serialization.XmlElementAttribute("RegistroRespuestaConsultaLRFactInformadasCliente", Order=0)]
        public RegistroRespuestaConsultaFactInformadasClienteType[] RegistroRespuestaConsultaLRFactInformadasCliente { get; set; }
    }
}