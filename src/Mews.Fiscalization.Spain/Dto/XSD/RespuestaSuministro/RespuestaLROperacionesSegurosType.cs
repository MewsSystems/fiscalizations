namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaSuministro
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
    public class RespuestaLROperacionesSegurosType : RespuestaComunAltaType
    {
        [System.Xml.Serialization.XmlElementAttribute("RespuestaLinea", Order=0)]
        public RespuestaOperacionesSegurosType[] RespuestaLinea { get; set; }
    }
}