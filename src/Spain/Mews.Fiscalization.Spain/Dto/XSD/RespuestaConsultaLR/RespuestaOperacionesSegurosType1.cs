using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.Xml.Serialization.XmlTypeAttribute(TypeName="RespuestaOperacionesSegurosType", Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RespuestaOperacionesSegurosType1
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public PersonaFisicaJuridicaType Contraparte { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ClaveOperacionType ClaveOperacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public string ImporteTotal { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public PersonaFisicaJuridicaUnicaESType EntidadSucedida { get; set; }
    }
}