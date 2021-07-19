using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroCobrosMetalicoType : RegistroSii
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public ContraparteConsultaType Contraparte { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public RangoFechaPresentacionType FechaPresentacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public FacturaModificadaType CobroModificado { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool CobroModificadoSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public LRFiltroCobrosMetalicoTypeClavePaginacion ClavePaginacion { get; set; }
    }
}