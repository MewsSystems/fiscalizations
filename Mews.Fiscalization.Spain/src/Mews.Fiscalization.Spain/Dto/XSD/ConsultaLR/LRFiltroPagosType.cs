using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroPagosType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IDFacturaRecibidaNombreBCType IDFactura { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public double ClavePaginacion { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool ClavePaginacionSpecified { get; set; }
    }
}