﻿using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;

namespace Mews.Fiscalization.Spain.Dto.XSD.SuministroLR
{
    [System.SerializableAttribute]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroLR.xsd")]
    public class LRBajaCobrosMetalicoType : RegistroSii
    {
        [System.Xml.Serialization.XmlElementAttribute(Order=0)]
        public PersonaFisicaJuridicaType Contraparte { get; set; }
    }
}