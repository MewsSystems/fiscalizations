namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class EstadoFactura2Type
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string TimestampUltimaModificacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public EstadoRegistroSIIType1 EstadoRegistro { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=2)]
        public string CodigoErrorRegistro { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string DescripcionErrorRegistro { get; set; }
    }
}