using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.ConsultaLR;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
public class LRFiltroBienInversionType : RegistroSii
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public IDFacturaConsulta1Type IDFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public RangoFechaPresentacionType FechaPresentacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public FacturaModificadaType FacturaModificada { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool FacturaModificadaSpecified { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public string IdentificacionBien { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public ClavePaginacionBienType ClavePaginacion { get; set; }
}