namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class IDFacturaImputacionType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IDFacturaImputacionTypeIDEmisorFactura IDEmisorFactura { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string NumSerieFacturaEmisor { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string NumSerieFacturaEmisorResumenFin { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string FechaExpedicionFacturaEmisor { get; set; }
    }
}