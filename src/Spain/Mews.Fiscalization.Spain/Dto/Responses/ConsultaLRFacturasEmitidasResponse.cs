﻿using Mews.Fiscalization.Spain.Dto.XSD.RespuestaConsultaLR;

namespace Mews.Fiscalization.Spain.Dto.Responses
{
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public class ConsultaLRFacturasEmitidasResponse
    {
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd", Order=0)]
        public RespuestaConsultaLRFacturasEmitidasType RespuestaConsultaLRFacturasEmitidas;

        public ConsultaLRFacturasEmitidasResponse()
        {
        }

        public ConsultaLRFacturasEmitidasResponse(RespuestaConsultaLRFacturasEmitidasType respuestaConsultaLRFacturasEmitidas)
        {
            RespuestaConsultaLRFacturasEmitidas = respuestaConsultaLRFacturasEmitidas;
        }
    }
}