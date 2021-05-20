using Mews.Fiscalization.Spain.Dto.Responses;
using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaSuministro
{
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLROperacionesSegurosType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRAgenciasViajesType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRIMetalicoType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLROComunitariasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRBienesInversionType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(RespuestaLRFRecibidasType))]
    [System.Xml.Serialization.XmlIncludeAttribute(typeof(SubmitIssuedInvoicesResponse))]
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd")]
    public class RespuestaComunAltaType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public string CSV { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public DatosPresentacionType DatosPresentacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public CabeceraSii Cabecera { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public EstadoEnvioType EstadoEnvio { get; set; }
    }
}