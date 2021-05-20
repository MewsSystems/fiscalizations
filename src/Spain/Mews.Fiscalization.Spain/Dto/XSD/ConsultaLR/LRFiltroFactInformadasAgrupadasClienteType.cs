﻿using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.ConsultaLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd")]
    public class LRFiltroFactInformadasAgrupadasClienteType : RegistroSiiImputacion
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public PersonaFisicaJuridicaUnicaESType Cliente { get; set; }

        [System.Xml.Serialization.XmlElementAttribute(Order=1)]
        public EstadoCuadreImputacionType EstadoCuadre { get; set; }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool EstadoCuadreSpecified { get; set; }
    }
}