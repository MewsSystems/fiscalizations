namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class BienDeInversionType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public string IdentificacionBien { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string FechaInicioUtilizacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public string ProrrataAnualDefinitiva { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
        public string RegularizacionAnualDeduccion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
        public string IdentificacionEntrega { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
        public string RegularizacionDeduccionEfectuada { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
        public string RefExterna { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
        public string NumRegistroAcuerdoFacturacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
        public PersonaFisicaJuridicaUnicaESType EntidadSucedida { get; set; }
    }
}