namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class ContraparteConsultaType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string NombreRazon { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("IDOtro", typeof(IDOtroType), Order=1)]
        [System.Xml.Serialization.XmlElementAttribute("NIF", typeof(string), Order=1)]
        public object Item { get; set; }
    }
}