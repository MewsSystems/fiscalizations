using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroOperacionesSegurosType : RegistroSii
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public ContraparteConsultaType Contraparte { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ClaveOperacionType ClaveOperacion { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool ClaveOperacionSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public RangoFechaPresentacionType FechaPresentacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public FacturaModificadaType OperacionModificada { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool OperacionModificadaSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public LRFiltroOperacionesSegurosTypeClavePaginacion ClavePaginacion { get; set; }
    }
}