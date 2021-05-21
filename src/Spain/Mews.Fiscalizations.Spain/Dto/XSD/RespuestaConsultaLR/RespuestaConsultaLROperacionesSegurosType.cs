namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaLROperacionesSegurosType : RespuestaConsultaLRFacturasType
    {
        [System.Xml.Serialization.XmlElementAttribute("RegistroRespuestaConsultaLROperacionesSeguros", Order=0)]
        public RegistroRespuestaConsultaOperacionesSegurosType[] RegistroRespuestaConsultaLROperacionesSeguros { get; set; }
    }
}