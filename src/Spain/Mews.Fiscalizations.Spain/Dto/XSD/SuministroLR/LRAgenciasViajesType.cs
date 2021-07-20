using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
    public class LRAgenciasViajesType : RegistroSii
    {
        [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
        public PersonaFisicaJuridicaType Contraparte { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
        public string ImporteTotal { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
        public PersonaFisicaJuridicaUnicaESType EntidadSucedida { get; set; }
    }
}