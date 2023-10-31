﻿using System.Security.Cryptography.X509Certificates;
using System.Xml;
using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Model;
using Mews.Fiscalizations.Core.Compression;
using Mews.Fiscalizations.Core.Xml;

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
    public async Task<SendInvoiceResponse> SendInvoiceAsync(TicketBaiInvoiceData invoiceData)
    {
        if (Region is Region.Bizkaia)
        {
            return await SendBizkaiaInvoiceAsync(invoiceData);
        }

        return await SendTicketBaiInvoiceAsync(invoiceData);
    }

    private async Task<SendInvoiceResponse> SendBizkaiaInvoiceAsync(TicketBaiInvoiceData invoiceData)
    {
        var ticketBaiInvoice = invoiceData.SignedRequest.OuterXml;
        var requestBody = await BizkaiaRequestHelper.CreateBizkaiaRequest(ticketBaiInvoice, ServiceInfo.Encoding);
        var requestContent = BizkaiaRequestHelper.CreateBizkaiaRequestContent(requestBody);
        var requestMessage = BizkaiaRequestHelper.CreateBizkaiaRequestMessage(ServiceInfo.SendInvoiceUri(Environment), requestContent, ticketBaiInvoice);

        var response = await HttpClient.SendAsync(requestMessage);

        var responseContent = await response.Content.ReadAsStringAsync();

        var responseDecompressed = await GzipCompressorHelper.DecompressAsync(Convert.FromBase64String(responseContent), ServiceInfo.Encoding, CancellationToken.None);
        var lroeResponse = XmlSerializer.Deserialize<LROEPJ240FacturasEmitidasConSGAltaRespuesta>(responseDecompressed);

        return DtoToModelConverter.Convert(
            response: lroeResponse,
            qrCodeUri: invoiceData.QrCodeUri,
            xmlRequestContent: invoiceData.SignedRequest.OuterXml,
            xmlResponseContent: responseContent,
            tbaiIdentifier: invoiceData.TbaiIdentifier,
            signatureValue: invoiceData.TrimmedSignature
        );
    }
    
    private async Task<SendInvoiceResponse> SendTicketBaiInvoiceAsync(TicketBaiInvoiceData invoiceData)
    {
        var signedRequest = invoiceData.SignedRequest;
        var requestContent = new StringContent(signedRequest.OuterXml, ServiceInfo.Encoding, "application/xml");
        var response = await HttpClient.PostAsync(ServiceInfo.SendInvoiceUri(Environment), requestContent);

        var responseContent = await response.Content.ReadAsStringAsync();
        var ticketBaiResponse = XmlSerializer.Deserialize<Dto.TicketBaiResponse>(responseContent);
        return DtoToModelConverter.Convert(
            response: ticketBaiResponse,
            qrCodeUri: invoiceData.QrCodeUri,
            xmlRequestContent: signedRequest.OuterXml,
            xmlResponseContent: responseContent,
            tbaiIdentifier: invoiceData.TbaiIdentifier,
            signatureValue: invoiceData.TrimmedSignature
        );
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
            signedRequest: signedRequest.OwnerDocument,
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

    private XmlElement GetSignedInvoiceDocument(SendInvoiceRequest request)
    {
        var ticketBaiRequest = ModelToDtoConverter.Convert(request, ServiceInfo);
        var xmlDoc = XmlSerializer.Serialize(ticketBaiRequest, new XmlSerializationParameters(
            encoding: ServiceInfo.Encoding,
            namespaces: NonEmptyEnumerable.Create(new XmlNamespace("http://www.w3.org/2000/09/xmldsig#"))
        ));
        xmlDoc.OwnerDocument.PreserveWhitespace = true;
        SigningService.SignXmlWithXadesBes(Certificate, xmlDoc.OwnerDocument, Region);
        return xmlDoc;
    }
}