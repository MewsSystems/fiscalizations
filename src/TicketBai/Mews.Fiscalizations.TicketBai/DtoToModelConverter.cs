using FuncSharp;
using Mews.Fiscalizations.TicketBai.Model;
using System;
using System.Linq;

namespace Mews.Fiscalizations.TicketBai
{
    internal static class DtoToModelConverter
    {
        public static SendInvoiceResponse Convert(Dto.TicketBaiResponse response, string qrCodeUri, string xmlRequestContent)
        {
            var result = response.Salida;
            return new SendInvoiceResponse(
                xmlRequestContent: xmlRequestContent,
                qrCodeUri: qrCodeUri,
                tbaiIdentifier: result.IdentificadorTBAI,
                received: DateTime.Parse(result.FechaRecepcion),
                state: result.Estado,
                description: result.Descripcion,
                stateExplanation: result.Azalpena,
                csv: result.CSV,
                validationResults: result.ResultadosValidacion?.Select(v => Convert(v))
            );
        }

        private static SendInvoiceValidationResult Convert(Dto.ResultadosValidacion validation)
        {
            return new SendInvoiceValidationResult(validation.Codigo, validation.Descripcion, validation.Azalpena);
        }
    }
}