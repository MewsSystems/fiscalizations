using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class RespuestaOperacionesSegurosBajaType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public RespuestaOperacionesSegurosBajaTypePeriodoLiquidacion PeriodoLiquidacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public PersonaFisicaJuridicaType Contraparte { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public ClaveOperacionType ClaveOperacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public EstadoRegistroType EstadoRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(DataType = "integer", Order = 4)]
    public string CodigoErrorRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 5)]
    public string DescripcionErrorRegistro { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 6)]
    public string CSV { get; set; }
}