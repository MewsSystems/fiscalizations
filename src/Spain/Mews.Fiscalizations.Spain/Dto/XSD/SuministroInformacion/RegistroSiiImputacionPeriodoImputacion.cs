namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public class RegistroSiiImputacionPeriodoImputacion
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string EjercicioImputacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public TimePeriodType PeriodoImputacion { get; set; }
}