﻿using System.Globalization;
using System.Net.Mime;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json;
using System.Xml;
using Mews.Fiscalizations.Basque.Dto;
using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;
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
        var signedRequest = invoiceData.SignedRequest;
        var requestContent = new StringContent(CreateRequest(signedRequest.OuterXml), ServiceInfo.Encoding, "application/xml");
        
        Region.Match(
            Region.Bizkaia, _ => AddBizkaiaHeaders(invoiceData.SignedRequest),
            _ => HttpClient.DefaultRequestHeaders.Clear()
            );
        
        var response = await HttpClient.PostAsync(ServiceInfo.SendInvoiceUri(Environment), requestContent);

        var responseContent = await response.Content.ReadAsStringAsync();
        var ticketBaiResponse = XmlSerializer.Deserialize<Dto.TicketBaiResponse>(responseContent);
        return DtoToModelConverter.Convert(
            response: ticketBaiResponse,
            qrCodeUri: invoiceData.QrCodeUri,
            xmlRequestContent: signedRequest.OuterXml,
            xmlResponseContent: responseContent,
            signatureValue: invoiceData.TrimmedSignature
        );
    }

    private void AddBizkaiaHeaders(XmlDocument signedRequest)
    {
        HttpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip");
        HttpClient.DefaultRequestHeaders.Add("Content-Encoding", "gzip");
        HttpClient.DefaultRequestHeaders.Add("Content-Length", signedRequest.OuterXml.Length.ToString(CultureInfo.InvariantCulture)); //TODO: this is not correct, fix later
        HttpClient.DefaultRequestHeaders.Add("Content-Type", MediaTypeNames.Application.Octet);
        HttpClient.DefaultRequestHeaders.Add("eus-bizkaia-n3-version", "1.0");
        HttpClient.DefaultRequestHeaders.Add("eus-bizkaia-n3-content-type", MediaTypeNames.Application.Xml);
        HttpClient.DefaultRequestHeaders.Add("eus-bizkaia-n3-data", CreateBizkaiaHeaderData(signedRequest));
    }

    private string CreateBizkaiaHeaderData(XmlDocument ticketBaiRequest)
    {
        var ticketBaiOriginalInvoice = XmlSerializer.Deserialize<TicketBai>(ticketBaiRequest.OuterXml);
        var bizkaiaHeaderData = new BizkaiaHeaderData
        {
            Issuer = new IssuerData
            {
                TaxpayerIdentificationNumber = ticketBaiOriginalInvoice.Sujetos.Emisor.NIF,
                FirstNameOrCompanyName = ticketBaiOriginalInvoice.Sujetos.Emisor.ApellidosNombreRazonSocial,
                Surname = string.Empty,
                SecondSurname = string.Empty
            },
            FiscalData = new FiscalData
            {
                FiscalYear = GetInvoiceDate(ticketBaiOriginalInvoice.Factura.CabeceraFactura.FechaExpedicionFactura).Year
            }
        };

        return JsonSerializer.Serialize(bizkaiaHeaderData);
    }

    private string CreateRequest(string requestXml)
    {
        if (Region == Region.Bizkaia)
        {
            return CreateBizkaiaRequest(requestXml);
        }

        return requestXml;
    }

    private string CreateBizkaiaRequest(string requestXml)
    {
        var lroeRequest = new LROEPJ240FacturasEmitidasConSGAltaPeticion
        {
            Cabecera = CreateBizkaiaHeaderRequest(requestXml),
            FacturasEmitidas = new FacturaEmitidaType[]
            {
                new FacturaEmitidaType
                {
                    TicketBai = Convert.ToBase64String(ServiceInfo.Encoding.GetBytes(requestXml))
                }
            }
        };
        string lroeRequestAsXml = XmlSerializer.Serialize(lroeRequest).OuterXml;
        var compressedBytes = lroeRequestAsXml.CompressAsync(ServiceInfo.Encoding, CancellationToken.None).Result;
        return Convert.ToBase64String(compressedBytes);
    }

    private Cabecera2 CreateBizkaiaHeaderRequest(string requestXml)
    {
        var ticketBaiOriginalInvoice = XmlSerializer.Deserialize<TicketBai>(requestXml);
        var invoiceDate = GetInvoiceDate(ticketBaiOriginalInvoice.Factura.CabeceraFactura.FechaExpedicionFactura);
        return new Cabecera2
        {
            Modelo = Modelo240Enum.Item240,
            Capitulo = CapituloModelo240Enum.Item1,
            Subcapitulo = SubcapituloModelo240Enum.Item11,
            Operacion = OperacionEnum.A00,
            Version = IDVersionEnum.Item10,
            Ejercicio = invoiceDate.Year,
            ObligadoTributario = new NIFPersonaType
            {
                NIF = ticketBaiOriginalInvoice.Sujetos.Emisor.NIF,
                ApellidosNombreRazonSocial = ticketBaiOriginalInvoice.Sujetos.Emisor.ApellidosNombreRazonSocial
            }
        };
    }

    private DateTime GetInvoiceDate(string invoiceDate)
    {
        return DateTime.Parse(invoiceDate, CultureInfo.InvariantCulture);
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