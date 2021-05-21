using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroFactInformadasClienteType : RegistroSiiImputacion
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public PersonaFisicaJuridicaUnicaESType Cliente { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string NumSerieFacturaEmisor { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public EstadoCuadreImputacionType EstadoCuadre { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool EstadoCuadreSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public RangoFechaType FechaExpedicion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public RangoFechaType FechaOperacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public ClavePaginacionClienteType ClavePaginacion { get; set; }
    }
}