using Mews.Fiscalization.Spain.Dto.XSD.ConsultaLR;

namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroFactInformadasAgrupadasProveedorType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroFactInformadasProveedorType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroFactInformadasAgrupadasClienteType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroFactInformadasClienteType))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
    public class RegistroSiiImputacion
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public RegistroSiiImputacionPeriodoImputacion PeriodoImputacion { get; set; }
    }
}