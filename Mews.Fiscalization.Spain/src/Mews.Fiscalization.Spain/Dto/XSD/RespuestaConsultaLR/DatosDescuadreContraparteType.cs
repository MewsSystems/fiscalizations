namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class DatosDescuadreContraparteType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string SumBaseImponibleISP { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string SumBaseImponible { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string SumCuota { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string SumCuotaRecargoEquivalencia { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string ImporteTotal { get; set; }
    }
}