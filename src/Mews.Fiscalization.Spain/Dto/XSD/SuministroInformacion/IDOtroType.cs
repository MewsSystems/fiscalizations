namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class IDOtroType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public CountryType2 CodigoPais { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool CodigoPaisSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public PersonaFisicaJuridicaIDTypeType IDType { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ID { get; set; }
    }
}