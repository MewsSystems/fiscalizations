using Mews.Fiscalizations.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalizations.Spain.Dto.XSD.RespuestaConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")]
    public class RegistroRespuestaConsultaFactInformadasClienteType
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IDFacturaImputacionType IDFactura { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public RegistroRespuestaConsultaFactInformadasClienteTypePeriodoLiquidacion PeriodoLiquidacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public FacturaRespuestaInformadaClienteType DatosFacturaInformadaCliente { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public PersonaFisicaJuridicaUnicaESType Cliente { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public EstadoFacturaImputacionType EstadoFactura { get; set; }
    }
}