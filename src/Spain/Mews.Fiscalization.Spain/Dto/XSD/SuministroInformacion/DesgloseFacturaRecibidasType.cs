namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class DesgloseFacturaRecibidasType
    {
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleIVA", IsNullable=false)]
        public DetalleIVARecibida2Type[] InversionSujetoPasivo { get; set; }

        [System.Xml.Serialization.XmlArrayAttribute(Order=1)]
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleIVA", IsNullable=false)]
        public DetalleIVARecibidaType[] DesgloseIVA { get; set; }
    }
}