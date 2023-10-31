using Mews.Fiscalizations.Basque.Dto.Bizkaia;
using Mews.Fiscalizations.Basque.Dto;
using Mews.Fiscalizations.Core.Compression;
using System.Globalization;
using System.Text;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Basque.Dto.Bizkaia.Header;
using System.Net.Mime;
using System.Text.Json;
using System.Net.Http.Headers;

namespace Mews.Fiscalizations.Basque;

public static class BizkaiaRequestHelper
{
    public async static Task<string> CreateBizkaiaRequest(string requestXml, Encoding encoding)
    {
        var lroeRequest = new LROEPJ240FacturasEmitidasConSGAltaPeticion
        {
            Cabecera = CreateBizkaiaHeaderRequest(requestXml),
            FacturasEmitidas = new FacturaEmitidaType[]
            {
                new FacturaEmitidaType
                {
                    TicketBai = Convert.ToBase64String(encoding.GetBytes(requestXml))
                }
            }
        };
        var lroeRequestAsXml = XmlSerializer.Serialize(lroeRequest).OuterXml;
        var compressedBytes = await lroeRequestAsXml.CompressAsync(encoding, CancellationToken.None);
        return Convert.ToBase64String(compressedBytes);
    }

    public static HttpRequestMessage CreateBizkaiaRequestMessage(Uri uri, StringContent stringContent, string ticketBaiRequest)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri);
        
        requestMessage.Content = stringContent;
        requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        requestMessage.Headers.TryAddWithoutValidation("eus-bizkaia-n3-version", "1.0");
        requestMessage.Headers.TryAddWithoutValidation("eus-bizkaia-n3-content-type", MediaTypeNames.Application.Xml);
        requestMessage.Headers.TryAddWithoutValidation("eus-bizkaia-n3-data", CreateBizkaiaHeaderData(ticketBaiRequest));

        return requestMessage;
    }

    public static StringContent CreateBizkaiaRequestContent(string requestBody)
    {
        var requestContent = new StringContent(requestBody);
        requestContent.Headers.ContentEncoding.Add("gzip");
        requestContent.Headers.ContentLength = requestBody.Length;
        requestContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Octet);

        return requestContent;
    }

    private static string CreateBizkaiaHeaderData(string ticketBaiRequest)
    {
        var ticketBaiOriginalInvoice = XmlSerializer.Deserialize<TicketBai>(ticketBaiRequest);
        var bizkaiaHeaderData = new BizkaiaHeaderData
        {
            Issuer = new IssuerData
            {
                TaxpayerIdentificationNumber = ticketBaiOriginalInvoice.Sujetos.Emisor.NIF,
                FirstNameOrCompanyName = ticketBaiOriginalInvoice.Sujetos.Emisor.ApellidosNombreRazonSocial
            },
            FiscalData = new FiscalData
            {
                FiscalYear = GetInvoiceDate(ticketBaiOriginalInvoice.Factura.CabeceraFactura.FechaExpedicionFactura).Year
            }
        };

        return JsonSerializer.Serialize(bizkaiaHeaderData);
    }

    private static Cabecera2 CreateBizkaiaHeaderRequest(string requestXml)
    {
        var ticketBaiOriginalInvoice = XmlSerializer.Deserialize<TicketBai>(requestXml);
        return new Cabecera2
        {
            Modelo = Modelo240Enum.Item240,
            Capitulo = CapituloModelo240Enum.Item1,
            Subcapitulo = SubcapituloModelo240Enum.Item11,
            Operacion = OperacionEnum.A00,
            Version = IDVersionEnum.Item10,
            Ejercicio = GetInvoiceDate(ticketBaiOriginalInvoice.Factura.CabeceraFactura.FechaExpedicionFactura).Year,
            ObligadoTributario = new NIFPersonaType
            {
                NIF = ticketBaiOriginalInvoice.Sujetos.Emisor.NIF,
                ApellidosNombreRazonSocial = ticketBaiOriginalInvoice.Sujetos.Emisor.ApellidosNombreRazonSocial
            }
        };
    }

    private static DateTime GetInvoiceDate(string invoiceDate)
    {
        return DateTime.ParseExact(invoiceDate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
    }
}
