using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.Bizkaia.Tests
{
    internal static class BatuzInvoiceRequestHelper
    {
        internal static LROEPJ240FacturasEmitidasConSGAltaPeticion CreateSampleBatuzRequest()
        {
            return new LROEPJ240FacturasEmitidasConSGAltaPeticion
            {
                Cabecera = new Cabecera2
                {
                    Modelo = "240",
                    Capitulo = "1",
                    Subcapitulo = "1.1",
                    Operacion = "A00",
                    Version = "1.0",
                    Ejercicio = "2022",
                    ObligadoTributario = new ObligadoTributarioType
                    {
                        NIF = "B00000034",
                        ApellidosNombreRazonSocial = "HOTEL ADIBIDEZ"
                    },
                },
                FacturasEmitidas = new FacturaEmitidaType[]
                {
                    new FacturaEmitidaType
                    {
                        TicketBai = "Some random invoice encoded in Base64"
                    },
                    new FacturaEmitidaType
                    {
                        TicketBai = "Another random invoice encoded in Base64"
                    }
                }
            };
        }
    }
}
