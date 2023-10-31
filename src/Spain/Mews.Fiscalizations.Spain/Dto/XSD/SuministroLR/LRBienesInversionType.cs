using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroLR;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
public class LRBienesInversionType : RegistroSii
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public IDFacturaComunitariaType IDFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public BienDeInversionType BienesInversion { get; set; }
}