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
    public async static Task<byte[]> CreateBizkaiaRequest(TicketBai ticketBaiInvoice, string ticketBaiInvoiceXml, Encoding encoding)
    {
        var lroeRequest = new LROEPJ240FacturasEmitidasConSGAltaPeticion
        {
            Cabecera = CreateBizkaiaHeaderRequest(ticketBaiInvoice),
            FacturasEmitidas = new FacturaEmitidaType[]
            {
                new FacturaEmitidaType
                {
                    TicketBai = Convert.ToBase64String(encoding.GetBytes(ticketBaiInvoiceXml))
                }
            }
        };
        var lroeRequestAsXml = XmlSerializer.Serialize(lroeRequest).OuterXml;
        return await lroeRequestAsXml.CompressAsync(encoding, CancellationToken.None);
    }

    public static HttpRequestMessage CreateBizkaiaRequestMessage(Uri uri, ByteArrayContent content, TicketBai ticketBaiInvoice)
    {
        var requestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
        {
            Content = content
        };

        requestMessage.Headers.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        requestMessage.Headers.TryAddWithoutValidation("eus-bizkaia-n3-version", "1.0");
        requestMessage.Headers.TryAddWithoutValidation("eus-bizkaia-n3-content-type", MediaTypeNames.Application.Xml);
        requestMessage.Headers.TryAddWithoutValidation("eus-bizkaia-n3-data", CreateBizkaiaHeaderData(ticketBaiInvoice));

        return requestMessage;
    }

    public static ByteArrayContent CreateBizkaiaRequestContent(byte[] requestBody)
    {
        var requestContent = new ByteArrayContent(requestBody);
        requestContent.Headers.ContentEncoding.Add("gzip");
        requestContent.Headers.ContentLength = requestBody.Length;
        requestContent.Headers.ContentType = new MediaTypeHeaderValue(MediaTypeNames.Application.Octet);

        return requestContent;
    }

    private static string CreateBizkaiaHeaderData(TicketBai ticketBaiOriginalInvoice)
    {
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

    private static Cabecera2 CreateBizkaiaHeaderRequest(TicketBai ticketBaiOriginalInvoice)
    {
        return new Cabecera2
        {
            Modelo = Modelo240Enum.Item240,
            Capitulo = CapituloModelo240Enum.Item1,
            Subcapitulo = SubcapituloModelo240Enum.Item11,
            SubcapituloSpecified = true,
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
