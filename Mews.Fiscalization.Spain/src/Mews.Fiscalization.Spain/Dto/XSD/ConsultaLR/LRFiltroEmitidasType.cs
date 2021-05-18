﻿using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroEmitidasType : RegistroSii
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public IDFacturaConsulta2Type IDFactura { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public ContraparteConsultaType Contraparte { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=2)]
        public RangoFechaPresentacionType FechaPresentacion { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=3)]
        public RangoFechaPresentacionType FechaCuadre { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=4)]
        public FacturaModificadaType FacturaModificada { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool FacturaModificadaSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=5)]
        public EstadoCuadreType EstadoCuadre { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool EstadoCuadreSpecified { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=6)]
        public IDFacturaExpedidaBCType ClavePaginacion { get; set; }
    }
}