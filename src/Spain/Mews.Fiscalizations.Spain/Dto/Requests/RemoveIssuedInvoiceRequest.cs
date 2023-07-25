using System.Xml.Serialization;
using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;
using Mews.Fiscalizations.Spain.Dto.XSD.SuministroLR;

namespace Mews.Fiscalizations.Spain.Dto.Requests
{
    [System.SerializableAttribute]
    [XmlRoot(ElementName = "BajaLRFacturasEmitidas", Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
    [XmlType(AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
    public class RemoveIssuedInvoiceRequest : SuministroInformacionBaja
    {
        [XmlElement("RegistroLRBajaExpedidas", Order = 0)]
        public LRBajaExpedidasType[] RegistroLRBajaExpedidas { get; set; }
    }
}