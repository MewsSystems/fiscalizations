namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class OperacionIntracomunitariaType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public OperacionIntracomunitariaTypeTipoOperacion TipoOperacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public OperacionIntracomunitariaTypeClaveDeclarado ClaveDeclarado { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public CountryMiembroType EstadoMiembro { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public string PlazoOperacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public string DescripcionBienes { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public string DireccionOperador { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public string FacturasODocumentacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public string RefExterna { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public string NumRegistroAcuerdoFacturacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public PersonaFisicaJuridicaUnicaESType EntidadSucedida { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=10)]
        public RegPrevioGGEEoREDEMEoCompetenciaType RegPrevioGGEEoREDEMEoCompetencia { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool RegPrevioGGEEoREDEMEoCompetenciaSpecified { get; set; }
    }
}