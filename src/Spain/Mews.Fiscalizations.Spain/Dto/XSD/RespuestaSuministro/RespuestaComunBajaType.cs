using Mews.Fiscalizations.Spain.Dto.Responses;
using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaSuministro;

[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaOperacionesSegurosType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaAgenciasViajesType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaIMetalicoType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaOComunitariasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaBienesInversionType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaFRecibidasType))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RemoveIssuedInvoicesResponse))]
[System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBajaFRecibidasPagosType))]
[System.SerializableAttribute]
[System.Xml.Serialization.XmlTypeAttribute(Namespace = "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
public class RespuestaComunBajaType
{
    [System.Xml.Serialization.XmlElementAttribute(Order = 0)]
    public string CSV { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 1)]
    public DatosPresentacionType DatosPresentacion { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 2)]
    public CabeceraSiiBaja Cabecera { get; set; }

    [System.Xml.Serialization.XmlElementAttribute(Order = 3)]
    public EstadoEnvioType EstadoEnvio { get; set; }
}