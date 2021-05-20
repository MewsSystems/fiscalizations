﻿using Mews.Fiscalization.Spain.Dto.XSD.ConsultaLR;

namespace Mews.Fiscalization.Spain.Dto.Requests
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public class ConsultaLRFactInformadasAgrupadasClienteRequest
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd", Order=0)]
        public ConsultaLRFactInformadasAgrupadasClienteType ConsultaLRFactInformadasAgrupadasCliente;

        public ConsultaLRFactInformadasAgrupadasClienteRequest()
        {
        }

        public ConsultaLRFactInformadasAgrupadasClienteRequest(ConsultaLRFactInformadasAgrupadasClienteType consultaLRFactInformadasAgrupadasCliente)
        {
            ConsultaLRFactInformadasAgrupadasCliente = consultaLRFactInformadasAgrupadasCliente;
        }
    }
}