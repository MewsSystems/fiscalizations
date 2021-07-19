namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class IDFacturaRecibidaTypeIDEmisorFactura
    {
        [System.Xml.Serialization.XmlElementAttribute("IDOtro", typeof(IDOtroType), Order = 0)]
        [System.Xml.Serialization.XmlElementAttribute("NIF", typeof(string), Order = 0)]
        public object Item { get; set; }
    }
}