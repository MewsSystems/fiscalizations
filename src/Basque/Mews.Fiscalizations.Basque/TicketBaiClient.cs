﻿using System.Net;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Mews.Fiscalizations.Core.Xml.Signing;
using Mews.Fiscalizations.Core.Xml.Signing.Crypto;
using Mews.Fiscalizations.Core.Xml.Signing.Signature;
using Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;
using Mews.Fiscalizations.Basque.Dto;
using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

namespace Mews.Fiscalizations.Basque;

public sealed class TicketBaiClient
{
    public TicketBaiClient(X509Certificate2 certificate, Region region, Environment environment)
    {
        Certificate = certificate;
        Environment = environment;
        Region = region;
        ServiceInfo = new ServiceInfo(region);

        var requestHandler = new HttpClientHandler();
        requestHandler.ClientCertificates.Add(certificate);
        if (region == Region.Bizkaia)
        {
            requestHandler.AutomaticDecompression = DecompressionMethods.GZip;
        }
        HttpClient = new HttpClient(requestHandler);
    }

    private HttpClient HttpClient { get; }

    private X509Certificate2 Certificate { get; }

    private Environment Environment { get; }

    private Region Region { get; }

    private ServiceInfo ServiceInfo { get; }

    /// <summary>
    /// Sends the invoice to the gov API using the provided invoiceData.
    /// </summary>
    /// <param name="invoiceData">TBAI invoice data can be generated by calling 'GetTicketBaiInvoiceData',
    /// or by using your own implementation. This allows you to either generate the QR data, and the signed request
    /// using your implementation or by using the library helpers.
    /// </param>
    /// <param name="cancellationToken">CancellationToken</param>
    public async Task<SendInvoiceResponse> SendInvoiceAsync(TicketBaiInvoiceData invoiceData, CancellationToken cancellationToken = default)
    {
        if (Region == Region.Bizkaia)
        {
            return await SendBizkaiaInvoiceAsync(invoiceData, cancellationToken);
        }

        return await SendTicketBaiInvoiceAsync(invoiceData);
    }

    private async Task<SendInvoiceResponse> SendBizkaiaInvoiceAsync(TicketBaiInvoiceData invoiceData, CancellationToken cancellationToken)
    {
        var ticketBaiInvoiceXml = invoiceData.SignedRequest.OuterXml;
        var ticketBaiInvoice = XmlSerializer.Deserialize<TicketBai>(ticketBaiInvoiceXml);
        var requestBody = await BizkaiaRequestHelpers.CreateBizkaiaRequest(ticketBaiInvoice, ticketBaiInvoiceXml, ServiceInfo.Encoding, cancellationToken);
        var requestMessage = BizkaiaRequestHelpers.CreateBizkaiaRequestMessage(ServiceInfo.SendInvoiceUri(Environment), requestBody, ticketBaiInvoice);

        var response = await HttpClient.SendAsync(requestMessage, cancellationToken);
        var responseContent = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response.IsSuccessStatusCode)
        {
            if (response.Headers.TryGetValues("eus-bizkaia-n3-tipo-respuesta", out var responseStatus))
            {
                if (responseStatus.First() != "Correcto" && string.IsNullOrEmpty(responseContent))
                {
                    var errorCode = string.Join(',', response.Headers.GetValues("eus-bizkaia-n3-codigo-respuesta"));
                    var errorMessage = string.Join(',', response.Headers.GetValues("eus-bizkaia-n3-mensaje-respuesta"));
                    return new SendInvoiceResponse(
                        xmlRequestContent: invoiceData.SignedRequest.OuterXml,
                        xmlResponseContent: "",
                        qrCodeUri: invoiceData.QrCodeUri,
                        tbaiIdentifier: invoiceData.TbaiIdentifier,
                        received: DateTime.Now,
                        state: InvoiceState.Refused,
                        description: errorMessage,
                        signatureValue: invoiceData.TrimmedSignature,
                        validationResults: [ new SendInvoiceValidationResult(MapErrorCode(errorCode), errorMessage) ]
                    );
                }

                var lroeResponse = XmlSerializer.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(responseContent);
                return DtoToModelConverter.Convert(
                    response: lroeResponse,
                    qrCodeUri: invoiceData.QrCodeUri,
                    xmlRequestContent: invoiceData.SignedRequest.OuterXml,
                    xmlResponseContent: responseContent,
                    tbaiIdentifier: invoiceData.TbaiIdentifier,
                    signatureValue: invoiceData.TrimmedSignature
                );
            }
        }
        return new SendInvoiceResponse(
            xmlRequestContent: invoiceData.SignedRequest.OuterXml,
            xmlResponseContent: "",
            qrCodeUri: invoiceData.QrCodeUri,
            tbaiIdentifier: invoiceData.TbaiIdentifier,
            received: DateTime.Now,
            state: InvoiceState.Refused,
            description: $"Response status code: {response.StatusCode}, Content: {responseContent}",
            signatureValue: invoiceData.TrimmedSignature,
            validationResults: [ new SendInvoiceValidationResult(ErrorCode.ServerErrorTryAgain, $"Response status code: {response.StatusCode}, Content: {responseContent}") ]
        );
    }

    private async Task<SendInvoiceResponse> SendTicketBaiInvoiceAsync(TicketBaiInvoiceData invoiceData)
    {
        var signedRequest = invoiceData.SignedRequest;
        var requestContent = new StringContent(signedRequest.OuterXml, ServiceInfo.Encoding, MediaTypeNames.Application.Xml);
        var response = await HttpClient.PostAsync(ServiceInfo.SendInvoiceUri(Environment), requestContent);

        var responseContent = await response.Content.ReadAsStringAsync();

        try
        {
            var ticketBaiResponse = XmlSerializer.Deserialize<TicketBaiResponse>(responseContent);
            return DtoToModelConverter.Convert(
                response: ticketBaiResponse,
                qrCodeUri: invoiceData.QrCodeUri,
                xmlRequestContent: signedRequest.OuterXml,
                xmlResponseContent: responseContent,
                tbaiIdentifier: invoiceData.TbaiIdentifier,
                signatureValue: invoiceData.TrimmedSignature
            );
        }
        catch (InvalidOperationException ex)
        {
            return new SendInvoiceResponse(
                xmlRequestContent: signedRequest.OuterXml,
                xmlResponseContent: "",
                qrCodeUri: invoiceData.QrCodeUri,
                tbaiIdentifier: invoiceData.TbaiIdentifier,
                received: DateTime.UtcNow,
                state: InvoiceState.Refused,
                description: "Server error. Please try again.",
                signatureValue: invoiceData.TrimmedSignature,
                validationResults: [
                    new SendInvoiceValidationResult(ErrorCode.ServerErrorTryAgain, $"Unhandled server error: {ex.Message}")
                ]
            );
        }
    }

    /// <summary>
    /// Generates the QR code URI, TBAI Identifier and the signed request document without actually calling the API.
    /// This allows displaying the required invoice data on your generated invoices without waiting for the API response.
    /// To report the invoice to the gov authorities, 'SendInvoiceAsync' must be used.
    /// </summary>
    /// <param name="request">Invoice request which will be mapped to Dto.TicketBai.</param>
    public TicketBaiInvoiceData GetTicketBaiInvoiceData(SendInvoiceRequest request)
    {
        var signedRequest = GetSignedInvoiceDocument(request);
        var signatureValue = signedRequest.GetElementsByTagName("ds:SignatureValue")[0].InnerText;
        var header = request.Invoice.Header;
        var tbaiIdentifier = GenerateTbaiIdentifier(signatureValue, request.Subject.Issuer.Nif.TaxpayerNumber, header.Issued);
        var qrCodeUri = QrCodeUriGenerator.Generate(
            serviceInfo: ServiceInfo,
            environment: Environment,
            tbaiIdentifier: tbaiIdentifier,
            invoiceSeries: header.Series,
            invoiceNumber: header.Number.Value,
            total: request.Invoice.InvoiceData.TotalAmount
        );
        return new TicketBaiInvoiceData(
            signedRequest: signedRequest,
            tbaiIdentifier: tbaiIdentifier,
            qrCodeUri: qrCodeUri,
            trimmedSignature: String1To100.CreateUnsafe(signatureValue.Substring(0, Math.Min(signatureValue.Length, 100)))
        );
    }

    private string GenerateTbaiIdentifier(string signature, string issuerTaxId, DateTime issued)
    {
        var trimmedSignature = signature.Substring(0, Math.Min(signature.Length, 13));
        var identifier = $"TBAI-{issuerTaxId}-{issued.ToString("ddMMyy")}-{trimmedSignature}-";
        var crc = QrCodeUriGenerator.GetCyclicRedundancyCheckDigits(identifier, ServiceInfo);
        return $"{identifier}{crc}";
    }

    private XmlDocument GetSignedInvoiceDocument(SendInvoiceRequest request)
    {
        var ticketBaiRequest = ModelToDtoConverter.Convert(request, ServiceInfo);
        var xmlDoc = XmlSerializer.Serialize(ticketBaiRequest, new XmlSerializationParameters(
            encoding: ServiceInfo.Encoding,
            namespaces: NonEmptyEnumerable.Create(new XmlNamespace("http://www.w3.org/2000/09/xmldsig#"))
        ));
        xmlDoc.OwnerDocument.PreserveWhitespace = true;
        return GetSignedInvoiceDocument(xmlDoc.OwnerDocument).Document;
    }

    private SignatureDocument GetSignedInvoiceDocument(XmlDocument doc)
    {
        var policyUri = Region.Match(
            Region.Bizkaia, _ => "https://www.batuz.eus/fitxategiak/batuz/ticketbai/sinadura_elektronikoaren_zehaztapenak_especificaciones_de_la_firma_electronica_v1_1.pdf",
            Region.Araba, _ => "https://ticketbai.araba.eus/tbai/sinadura",
            Region.Gipuzkoa, _ => "https://www.gipuzkoa.eus/ticketbai/sinadura"
        );
        var policyHash = Region.Match(
            Region.Bizkaia, _ => "K2baIY0fk8jbkPHkffk5F5C46O5VuzDwH21dAovjVRs=",
            Region.Araba, _ => "4Vk3uExj7tGn9DyUCPDsV9HRmK6KZfYdRiW3StOjcQA=",
            Region.Gipuzkoa, _ => "vSe1CH7eAFVkGN0X2Y7Nl9XGUoBnziDA5BGUSsyt8mg="
        );

        var signatureParameters = new SignatureParameters(
            xmlDocumentToSign: doc,
            signer: new Signer(Certificate),
            digestMethod: DigestMethod.SHA256,
            signatureMethod: SignatureMethod.RSAwithSHA256,
            dataFormat: new DataFormat(MimeType: "text/xml"),
            signerRole: new SignerRole(Certificate.ToEnumerable()),
            signingDate: DateTime.Now,
            signaturePolicyInfo: new SignaturePolicyInfo(policyUri, policyHash, DigestMethod.SHA256, policyUri),
            elementIdToSign: Guid.NewGuid().ToString()
        );
        var signatureDocument = XadesService.SignEnveloped(signatureParameters);
        var isValidSignature = signatureDocument.XadesSignature.XadesCheckSignature(XadesCheckSignatureMasks.AllChecks, DigestMethod.SHA256.GetHashAlgorithm());
        if (!isValidSignature)
        {
            throw new Exception("Invalid signature.");
        }
        return signatureDocument;
    }

    private static ErrorCode MapErrorCode(string code)
    {
        return code switch
        {
            "N3_0000001" => ErrorCode.InvalidIssuerCertificate,
            "N3_0000002" => ErrorCode.InvalidIssuerNameOrNif,
            "N3_0000010" => ErrorCode.InvalidIssuerNif,
            _ => throw new NotImplementedException($"{code} is not implemented.")
        };
    }
}