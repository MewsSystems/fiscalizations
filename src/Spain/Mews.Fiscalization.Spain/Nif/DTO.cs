using System.Xml.Serialization;

namespace Mews.Fiscalizations.Spain.Nif
{
    [XmlType(AnonymousType = true, Namespace = "http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Ent.xsd")]
    public class VNifV2EntContribuyente
    {
        [XmlElement(Order=0)]
        public string Nif { get; set; }

        [XmlElement(Order=1)]
        public string Nombre { get; set; }
    }

    [XmlType(AnonymousType=true, Namespace="http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Sal.xsd")]
    public class VNifV2SalContribuyente
    {
        [XmlElement(Order=0)]
        public string Nif { get; set; }

        [XmlElement(Order=1)]
        public string Nombre { get; set; }

        [XmlElement(Order=2)]
        public string Resultado { get; set; }
    }

    [XmlRoot(ElementName = "VNifV2Ent", Namespace = "http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Ent.xsd")]
    public class Entrada
    {
        [XmlElement("Contribuyente", Order = 0)]
        public VNifV2EntContribuyente[] Contribuyente { get; set; }
    }

    [XmlRoot(ElementName = "VNifV2Sal", Namespace = "http://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/burt/jdit/ws/VNifV2Sal.xsd")]
    public class Salida
    {
        [XmlElement("Contribuyente", Order = 0)]
        public VNifV2SalContribuyente[] Contribuyente { get; set; }
    }
}