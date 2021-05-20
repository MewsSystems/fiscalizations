using System;
using System.Globalization;
using System.Linq;
using FuncSharp;
using Mews.Fiscalization.Spain.Dto.Responses;
using Mews.Fiscalization.Spain.Dto.XSD.RespuestaSuministro;
using Mews.Fiscalization.Spain.Dto.XSD.SuministroInformacion;
using Mews.Fiscalization.Spain.Model;
using Mews.Fiscalization.Spain.Model.Response;

namespace Mews.Fiscalization.Spain.Converters
{
    internal class DtoToModelConverter
    {
        public ReceivedInvoices Convert(SubmitIssuedInvoicesResponse response)
        {
            return new ReceivedInvoices(
                Convert(response.Cabecera),
                Convert(response.EstadoEnvio),
                response.RespuestaLinea.Select(i => Convert(i)).ToArray(),
                response.CSV.ToNonEmptyOption().GetOrNull());
        }

        private InvoiceResult Convert(RespuestaExpedidaType invoice)
        {
            var result = Convert(invoice.EstadoRegistro);
            return new InvoiceResult(
                id: Convert(invoice.IDFactura),
                result: result,
                errorCode: invoice.CodigoErrorRegistro.ToInt().Match(c => (int?)c, _ => null),
                errorMessage: invoice.DescripcionErrorRegistro.ToNonEmptyOption().GetOrNull(),
                secureVerificationCodeForOriginalInvoice: invoice.CSV.ToNonEmptyOption().GetOrNull()
            );
        }

        private InvoiceId Convert(IDFacturaExpedidaType iDFactura)
        {
            return new InvoiceId(
                issuer: iDFactura.IDEmisorFactura.NIF,
                number: iDFactura.NumSerieFacturaEmisor,
                date: ConvertDate(iDFactura.FechaExpedicionFacturaEmisor)
            );
        }

        private Header Convert(CabeceraSii cabecera)
        {
            return new Header(
                company: new LocalCompany(name: cabecera.Titular.NombreRazon, taxpayerNumber: cabecera.Titular.NIF),
                communicationType: cabecera.TipoComunicacion.Match(
                    ClaveTipoComunicacionType.A0, _ => CommunicationType.Registration,
                    ClaveTipoComunicacionType.A1, _ => CommunicationType.Amendment,
                    ClaveTipoComunicacionType.A4, _ => CommunicationType.AmendmentForTravellers
                )
            );
        }

        private RegisterResult Convert(EstadoEnvioType estadoEnvio)
        {
            return estadoEnvio.Match(
                EstadoEnvioType.Correcto, _ => RegisterResult.Correct,
                EstadoEnvioType.ParcialmenteCorrecto, _ => RegisterResult.PartialyIncorrect,
                EstadoEnvioType.Incorrecto, _ => RegisterResult.AllIncorrect
            );
        }

        private InvoiceRegisterResult Convert(EstadoRegistroType estadoRegistro)
        {
            return estadoRegistro.Match(
                EstadoRegistroType.Correcto, _ => InvoiceRegisterResult.Accepted,
                EstadoRegistroType.AceptadoConErrores, _ => InvoiceRegisterResult.AcceptedWithErrors,
                EstadoRegistroType.Incorrecto, _ => InvoiceRegisterResult.Rejected
            );
        }

        private DateTime ConvertDate(string date)
        {
            return DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        }
    }
}
