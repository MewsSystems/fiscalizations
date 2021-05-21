using System.Xml.Serialization;
using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;
using Mews.Fiscalizations.Spain.Dto.XSD.SuministroLR;

namespace Mews.Fiscalizations.Spain.Dto.Requests
{
    [System.SerializableAttribute]
    [XmlRoot(ElementName = "SuministroLRFacturasEmitidas", Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
    [XmlType(TypeName = "SuministroLRFacturasEmitidas", AnonymousType = true, Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
    public class SubmitIssuedInvoicesRequest : SuministroInformacion
    {
        [XmlElement("RegistroLRFacturasEmitidas", Order=0)]
        public LRfacturasEmitidasType[] RegistroLRFacturasEmitidas { get; set; }
    }
}
