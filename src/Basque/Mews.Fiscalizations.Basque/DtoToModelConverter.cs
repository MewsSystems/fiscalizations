using Mews.Fiscalizations.Basque.Model;
using System.Globalization;

namespace Mews.Fiscalizations.Basque;

internal static class DtoToModelConverter
{
    public static SendInvoiceResponse Convert(
        Dto.TicketBaiResponse response,
        string qrCodeUri,
        string xmlRequestContent,
        string xmlResponseContent,
        String1To100 signatureValue)
    {
        var result = response.Salida;
        return new SendInvoiceResponse(
            xmlRequestContent: xmlRequestContent,
            xmlResponseContent: xmlResponseContent,
            qrCodeUri: qrCodeUri,
            tbaiIdentifier: result.IdentificadorTBAI,
            received: DateTime.ParseExact(result.FechaRecepcion, "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture),
            state: ParseEnum<InvoiceState>(result.Estado),
            description: result.Descripcion,
            stateExplanation: result.Azalpena,
            signatureValue: signatureValue,
            csv: result.CSV,
            validationResults: result.ResultadosValidacion?.Select(v => Convert(v))
        );
    }

    private static SendInvoiceValidationResult Convert(Dto.ResultadosValidacion validation)
    {
        return new SendInvoiceValidationResult(ParseEnum<ErrorCode>(validation.Codigo), validation.Descripcion, validation.Azalpena);
    }

    private static T ParseEnum<T>(string value)
        where T : struct
    {
        return Enum.TryParse<T>(value, out var result).Match(
            u => result,
            _ => throw new NotImplementedException($"{value} is not implemented in {typeof(T).Name}.")
        );
    }
}