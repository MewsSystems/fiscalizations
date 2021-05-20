using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class FacturaRespuestaExpedidaType : FacturaRespuestaType
    {
        [System.Xml.Serialization.XmlArrayAttribute(Order=0)]
        [System.Xml.Serialization.XmlArrayItemAttribute("DetalleInmueble", IsNullable=false)]
        public DatosInmuebleType[] DatosInmueble { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public string ImporteTransmisionInmueblesSujetoAIVA { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public EmitidaPorTercerosType EmitidaPorTercerosODestinatario { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool EmitidaPorTercerosODestinatarioSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public EmitidaPorTercerosType FacturacionDispAdicionalTerceraYsextayDelMercadoOrganizadoDelGas { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool FacturacionDispAdicionalTerceraYsextayDelMercadoOrganizadoDelGasSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public VariosDestinatariosType VariosDestinatarios { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool VariosDestinatariosSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public CuponType Cupon { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool CuponSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute("FacturaSinIdentifDestinatarioAritculo6.1.d", Order=6)]
        public CompletaSinDestinatarioType FacturaSinIdentifDestinatarioAritculo61d { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool FacturaSinIdentifDestinatarioAritculo61dSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=7)]
        public PersonaFisicaJuridicaType Contraparte { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=8)]
        public FacturaRespuestaExpedidaTypeTipoDesglose TipoDesglose { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=9)]
        public FacturaARType Cobros { get; set; }
    }
}