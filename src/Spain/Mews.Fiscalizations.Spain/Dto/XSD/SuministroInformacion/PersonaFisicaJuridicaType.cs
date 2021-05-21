namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class PersonaFisicaJuridicaType
    {
        public PersonaFisicaJuridicaType()
        {
        }
        public PersonaFisicaJuridicaType(string nif, string nombreRazon)
            : this(nombreRazon)
        {
            Item = nif;
        }
        public PersonaFisicaJuridicaType(IDOtroType idOther, string nombreRazon)
            : this(nombreRazon)
        {
            Item = idOther;
        }
        private PersonaFisicaJuridicaType(string nombreRazon)
            : this()
        {
            NombreRazon = nombreRazon;
        }

        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string NombreRazon { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string NIFRepresentante { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("IDOtro", typeof(IDOtroType), Order=2)]
        [System.Xml.Serialization.XmlElementAttribute("NIF", typeof(string), Order=2)]
        public object Item { get; set; }
    }
}