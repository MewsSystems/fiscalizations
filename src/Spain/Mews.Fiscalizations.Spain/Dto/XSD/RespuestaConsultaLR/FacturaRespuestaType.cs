using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR;

[System.Xml.Serialization.XmlIncludeAttribute(typeof(FacturaRespuestaInformadaProveedorType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FacturaRespuestaInformadaClienteType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FacturaRespuestaRecibidaType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(FacturaRespuestaExpedidaType))]
[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
public class FacturaRespuestaType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public ClaveTipoFacturaType TipoFactura { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public ClaveTipoRectificativaType TipoRectificativa { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool TipoRectificativaSpecified { get; set; }

    [System.Xml.Serialization.XmlArrayAttribute(Order = 2)]
    [System.Xml.Serialization.XmlArrayItemAttribute("IDFacturaAgrupada", IsNullable = false)]
    public IDFacturaARType[] FacturasAgrupadas { get; set; }

    [System.Xml.Serialization.XmlArrayAttribute(Order = 3)]
    [System.Xml.Serialization.XmlArrayItemAttribute("IDFacturaRectificada", IsNullable = false)]
    public IDFacturaARType[] FacturasRectificadas { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 4)]
    public DesgloseRectificacionType ImporteRectificacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string FechaOperacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public IdOperacionesTrascendenciaTributariaType ClaveRegimenEspecialOTrascendencia { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 7)]
    public IdOperacionesTrascendenciaTributariaType ClaveRegimenEspecialOTrascendenciaAdicional1 { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ClaveRegimenEspecialOTrascendenciaAdicional1Specified { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 8)]
    public IdOperacionesTrascendenciaTributariaType ClaveRegimenEspecialOTrascendenciaAdicional2 { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool ClaveRegimenEspecialOTrascendenciaAdicional2Specified { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 9)]
    public string NumRegistroAcuerdoFacturacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 10)]
    public string ImporteTotal { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 11)]
    public string BaseImponibleACoste { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 12)]
    public string DescripcionOperacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 13)]
    public string RefExterna { get; set; }

    [System.Xml.Serialization.XmlElementAttribute("FacturaSimplificadaArticulos7.2_7.3", Order = 14)]
    public SimplificadaCualificadaType FacturaSimplificadaArticulos72_73 { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool FacturaSimplificadaArticulos72_73Specified { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 15)]
    public PersonaFisicaJuridicaUnicaESType EntidadSucedida { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 16)]
    public RegPrevioGGEEoREDEMEoCompetenciaType RegPrevioGGEEoREDEMEoCompetencia { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool RegPrevioGGEEoREDEMEoCompetenciaSpecified { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 17)]
    public MacrodatoType Macrodato { get; set; }

    [System.Xml.Serialization.XmlIgnoreAttribute]
    public bool MacrodatoSpecified { get; set; }
}