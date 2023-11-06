using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Model;
using System.Globalization;
using System.Net.Http.Headers;

namespace Mews.Fiscalizations.Basque;

public static class DtoToModelConverter
{
    internal static SendInvoiceResponse Convert(
        Dto.TicketBaiResponse response,
        string qrCodeUri,
        string xmlRequestContent,
        string xmlResponseContent,
        string tbaiIdentifier,
        String1To100 signatureValue)
    {
        var result = response.Salida;
        return new SendInvoiceResponse(
            xmlRequestContent: xmlRequestContent,
            xmlResponseContent: xmlResponseContent,
            qrCodeUri: qrCodeUri,
            tbaiIdentifier: tbaiIdentifier,
            received: DateTime.ParseExact(result.FechaRecepcion, "dd-MM-yyyy H:mm:ss", CultureInfo.InvariantCulture),
            state: ParseEnum<InvoiceState>(result.Estado),
            description: result.Descripcion,
            signatureValue: signatureValue,
            validationResults: result.ResultadosValidacion?.Select(v => Convert(v))
        );
    }

    internal static SendInvoiceResponse Convert(
        LROEPJ240FacturasEmitidasConSGAltaRespuesta response,
        string qrCodeUri,
        string xmlRequestContent,
        string xmlResponseContent,
        string tbaiIdentifier,
        String1To100 signatureValue)
    {
        var record = response.Registros.Single();
        var recordStatus = record.SituacionRegistro;
        var invoiceIdentifier = record.Identificador.Item as IDFacturaType; // TODO: check if they would ever return byte[] ticketbai.
        return new SendInvoiceResponse(
            xmlRequestContent: xmlRequestContent,
            xmlResponseContent: xmlResponseContent,
            qrCodeUri: qrCodeUri,
            tbaiIdentifier: tbaiIdentifier,
            received: DateTime.ParseExact(invoiceIdentifier.FechaExpedicionFactura, "dd-MM-yyyy", CultureInfo.InvariantCulture),
            state: ConvertBizkaiaState(recordStatus.EstadoRegistro),
            description: recordStatus.DescripcionErrorRegistroES,
            signatureValue: signatureValue,
            validationResults: Convert(recordStatus).ToEnumerable()
        );
    }

    public static ErrorCode ConvertBizkaiaErrorCodes(string bizkaiaErrorCode)
    {
        return bizkaiaErrorCode switch
        {
            "B4_2000001" => ErrorCode.XsdSchemeViolation,
            "B4_1000001" => ErrorCode.XsdSchemeViolation,
            "B4_2000003" => ErrorCode.DuplicateInvoice,
            "B4_1000005" => ErrorCode.DuplicateInvoice,
            "B4_2000000" => ErrorCode.MissingMandatoryData,
            "B4_1000004" => ErrorCode.ServerErrorTryAgain,
            "B4_2000013" => ErrorCode.TaxIdentifierCountryCodeDoesntMatchCountryCodeField,
            "B4_2000011" => ErrorCode.InvalidReceiverTaxIdentifierFormat,
            "B4_2000012" => ErrorCode.MissingCountryCode,
            "B4_2000008" => ErrorCode.InvoiceIssueDateGreaterThanCurrentDate,
            "B4_2000016" => ErrorCode.CorrectedInvoiceMustNotBeReportedWhenCorrectiveInvoiceIsNotReported,
            "B4_2000045" => ErrorCode.CorrectedInvoiceNotIndicated,
            "B4_2000070" => ErrorCode.InvalidSignatureOrSigningCertificate,
            "B4_2000060" => ErrorCode.InvalidOrMissingInvoiceChain,
            "B4_2000061" => ErrorCode.InvalidOrMissingInvoiceChain,
            "B4_2000038" => ErrorCode.BreakdownMustHaveProvisionOrDeliveryOrBoth,
            "B4_2000030" => ErrorCode.InvoiceMustContainAtLeastOneExemptOrNonExemptParty,
            _ => throw new NotImplementedException($"We currently have no support for this {bizkaiaErrorCode} error code.")
        };
    }

    public static InvoiceState ConvertBizkaiaState(EstadoRegistroEnum state)
    {
        // TODO: revisit to confirm that we are handling all the statuses correctly.
        return state.Match(
            EstadoRegistroEnum.Correcto, _ => InvoiceState.Received,
            _ => InvoiceState.Refused
        );
    }

    private static SendInvoiceValidationResult Convert(Dto.ResultadosValidacion validation)
    {
        return new SendInvoiceValidationResult(ParseEnum<ErrorCode>(validation.Codigo), validation.Descripcion);
    }

    private static SendInvoiceValidationResult Convert(SituacionRegistroType situacionRegistro)
    {
        return new SendInvoiceValidationResult(ConvertBizkaiaErrorCodes(situacionRegistro.CodigoErrorRegistro), situacionRegistro.DescripcionErrorRegistroES);
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