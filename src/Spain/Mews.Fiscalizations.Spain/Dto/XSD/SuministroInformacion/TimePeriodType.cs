﻿namespace Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(TypeName = "TipoPeriodoType", Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd")]
public enum TimePeriodType
{
    [System.Xml.Serialization.XmlEnumAttribute("01")]
    January,
    [System.Xml.Serialization.XmlEnumAttribute("02")]
    February,
    [System.Xml.Serialization.XmlEnumAttribute("03")]
    March,
    [System.Xml.Serialization.XmlEnumAttribute("04")]
    April,
    [System.Xml.Serialization.XmlEnumAttribute("05")]
    May,
    [System.Xml.Serialization.XmlEnumAttribute("06")]
    June,
    [System.Xml.Serialization.XmlEnumAttribute("07")]
    July,
    [System.Xml.Serialization.XmlEnumAttribute("08")]
    August,
    [System.Xml.Serialization.XmlEnumAttribute("09")]
    September,
    [System.Xml.Serialization.XmlEnumAttribute("10")]
    October,
    [System.Xml.Serialization.XmlEnumAttribute("11")]
    November,
    [System.Xml.Serialization.XmlEnumAttribute("12")]
    December,
    [System.Xml.Serialization.XmlEnumAttribute("0A")]
    FullYear,
    [System.Xml.Serialization.XmlEnumAttribute("1T")]
    Q1,
    [System.Xml.Serialization.XmlEnumAttribute("2T")]
    Q2,
    [System.Xml.Serialization.XmlEnumAttribute("3T")]
    Q3,
    [System.Xml.Serialization.XmlEnumAttribute("4T")]
    Q4,
}