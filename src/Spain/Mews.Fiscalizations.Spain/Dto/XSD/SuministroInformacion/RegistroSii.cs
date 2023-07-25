using Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR;
using Mews.Fiscalizations.Spain.Dto.XSD.SuministroLR;

namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroAgenciasViajesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroCobrosMetalicoType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroOperacionesSegurosType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroDetOperIntracomunitariasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroBienInversionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroRecibidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFiltroEmitidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaOperacionIntracomunitariaType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LROperacionIntracomunitariaType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaRegistroLROperacionesSegurosType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LROperacionesSegurosType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaCobrosMetalicoType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRCobrosMetalicoType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaAgenciasViajesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRAgenciasViajesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaBienesInversionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBienesInversionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaRecibidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRFacturasRecibidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRBajaExpedidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(LRfacturasEmitidasType))]
[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class RegistroSii
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public RegistroSiiPeriodoLiquidacion PeriodoLiquidacion { get; set; }
}