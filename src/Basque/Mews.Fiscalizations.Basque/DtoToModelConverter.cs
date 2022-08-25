using FuncSharp;
using Mews.Fiscalizations.Basque.Model;
using System;
using System.Globalization;
using System.Linq;

namespace Mews.Fiscalizations.Basque
{
    internal static class DtoToModelConverter
    {
        public static SendInvoiceResponse Convert(Dto.TicketBaiResponse response, string qrCodeUri, string xmlRequestContent, string xmlResponseContent)
        {
            var result = response.Salida;
            return new SendInvoiceResponse(
                xmlRequestContent: xmlRequestContent,
                xmlResponseContent: xmlResponseContent,
                qrCodeUri: qrCodeUri,
                tbaiIdentifier: result.IdentificadorTBAI,
                received: DateTime.Parse(result.FechaRecepcion, CultureInfo.InvariantCulture),
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