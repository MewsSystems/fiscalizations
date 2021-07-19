namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaConsultaCobrosType : RespuestaConsultaFacturaCobrosType
    {
        [System.Xml.Serialization.XmlElementAttribute("RegistroRespuestaConsultaCobros", Order = 0)]
        public RegistroRespuestaConsultaCobrosType[] RegistroRespuestaConsultaCobros { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public double ClavePaginacion { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool ClavePaginacionSpecified { get; set; }
    }
}