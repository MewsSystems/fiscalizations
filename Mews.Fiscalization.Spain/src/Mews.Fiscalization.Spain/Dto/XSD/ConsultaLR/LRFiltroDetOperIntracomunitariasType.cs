using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroDetOperIntracomunitariasType : RegistroSii
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IDFacturaConsulta1Type IDFactura { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public RangoFechaPresentacionType FechaPresentacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public FacturaModificadaType FacturaModificada { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool FacturaModificadaSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public IDFacturaComunitariaType ClavePaginacion { get; set; }
    }
}