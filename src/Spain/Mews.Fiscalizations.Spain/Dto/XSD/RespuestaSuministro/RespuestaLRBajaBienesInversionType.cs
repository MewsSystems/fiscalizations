﻿namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

[Serializable]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class RespuestaLRBajaBienesInversionType : RespuestaComunBajaType
{
    [System.Xml.Serialization.XmlElementAttribute("RespuestaLinea", Order = 0)]
    public RespuestaBienBajaType[] RespuestaLinea { get; set; }
}