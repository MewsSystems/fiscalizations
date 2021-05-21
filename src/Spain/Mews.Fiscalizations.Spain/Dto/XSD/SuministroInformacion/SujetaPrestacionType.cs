namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class SujetaPrestacionType
    {
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleExenta", IsNullable=false)]
        public DetalleExentaType[] Exenta { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public SujetaPrestacionTypeNoExenta NoExenta { get; set; }
    }
}