namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class TipoSinDesglosePrestacionType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public SujetaPrestacionType Sujeta { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public NoSujetaType NoSujeta { get; set; }
    }
}