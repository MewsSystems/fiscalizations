using Mews.Fiscalizations.Basque.Dto.Bizkaia;
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

    public static SendInvoiceResponse Convert(
        LROEPJ240FacturasEmitidasConSGAltaRespuesta response,
        string qrCodeUri,
        string xmlRequestContent,
        string xmlResponseContent,
        String1To100 signatureValue
        )
    {
        var invoiceIdentifier = response.Registros.Single().Identificador.Item as IDFacturaType;
        var invoiceState = response.Registros.Single().SituacionRegistro;
        return new SendInvoiceResponse(
            xmlRequestContent: xmlRequestContent,
            xmlResponseContent: xmlResponseContent,
            qrCodeUri: qrCodeUri,
            tbaiIdentifier: invoiceIdentifier.NumFactura,
            received: DateTime.ParseExact(invoiceIdentifier.FechaExpedicionFactura, "dd-MM-yyyy", CultureInfo.InvariantCulture),
            state: ConvertBizkaiaState(invoiceState.EstadoRegistro),
            description: invoiceState.DescripcionErrorRegistroES,
            stateExplanation: string.Empty,
            signatureValue: signatureValue,
            csv: string.Empty,
            validationResults: response.Registros?.Select(v => Convert(v.SituacionRegistro))
        );
    }

    private static SendInvoiceValidationResult Convert(Dto.ResultadosValidacion validation)
    {
        return new SendInvoiceValidationResult(ParseEnum<ErrorCode>(validation.Codigo), validation.Descripcion, validation.Azalpena);
    }

    private static SendInvoiceValidationResult Convert(SituacionRegistroType situacionRegistro)
    {
        return new SendInvoiceValidationResult(
            errorCode: ConvertBizkaiaErrorCodes(situacionRegistro.CodigoErrorRegistro), 
            description: situacionRegistro.DescripcionErrorRegistroEU, 
            explanation: string.Empty
        );
    }

    private static T ParseEnum<T>(string value)
        where T : struct
    {
        return Enum.TryParse<T>(value, out var result).Match(
            u => result,
            _ => throw new NotImplementedException($"{value} is not implemented in {typeof(T).Name}.")
        );
    }
    
    private static InvoiceState ConvertBizkaiaState(EstadoRegistroEnum state)
    {
        return state.Match(
            EstadoRegistroEnum.Correcto, _ => InvoiceState.Received,
            EstadoRegistroEnum.AceptadoConErrores, _ => InvoiceState.Received,
            EstadoRegistroEnum.Incorrecto, _ => InvoiceState.Refused,
            EstadoRegistroEnum.Anulado, _ => InvoiceState.Refused
        );
    }

    private static ErrorCode ConvertBizkaiaErrorCodes(string bizkaiaErrorCode)
    {
        switch (bizkaiaErrorCode)
        {
            case "B4_2000001":
            case "B4_1000001": return ErrorCode.XsdSchemeViolation;
            case "B4_2000003":
            case "B4_1000005": return ErrorCode.DuplicateInvoice;
            case "B4_2000000": return ErrorCode.MissingMandatoryData;
            case "B4_1000004": return ErrorCode.ServerErrorTryAgain;
            case "B4_2000013": return ErrorCode.TaxIdentifierCountryCodeDoesntMatchCountryCodeField;
            case "B4_2000011": return ErrorCode.InvalidReceiverTaxIdentifierFormat;
            case "B4_2000012": return ErrorCode.MissingCountryCode;
            case "B4_2000008": return ErrorCode.InvoiceIssueDateGreaterThanCurrentDate;
            case "B4_2000016": return ErrorCode.CorrectedInvoiceMustNotBeReportedWhenCorrectiveInvoiceIsNotReported;
            case "B4_2000045": return ErrorCode.CorrectedInvoiceNotIndicated;
            case "B4_2000070": return ErrorCode.InvalidSignatureOrSigningCertificate;
            case "B4_2000060": 
            case "B4_2000061": return ErrorCode.InvalidOrMissingInvoiceChain;
            case "B4_2000038": return ErrorCode.BreakdownMustHaveProvisionOrDeliveryOrBoth;
            case "B4_2000030": return ErrorCode.InvoiceMustContainAtLeastOneExemptOrNonExemptParty;

            default: throw new NotImplementedException("We currently have no support for this error code.");
            
        }
    }
}