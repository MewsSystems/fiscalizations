using FuncSharp;
using Mews.Fiscalizations.Basque.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Basque;

internal sealed class ServiceInfo
{
    public ServiceInfo(Region region)
    {
        Version = IDVersionTicketBaiType1.Item12;
        InvoiceBaseUrls = region.Match(
            Region.Gipuzkoa, _ => new Dictionary<Environment, Uri>
            {
                [Environment.Test] = new Uri("https://tbai-z.prep.gipuzkoa.eus"),
                [Environment.Production] = new Uri("https://tbai-z.egoitza.gipuzkoa.eus")
            },
            Region.Araba, _ => new Dictionary<Environment, Uri>
            {
                [Environment.Test] = new Uri("https://pruebas-ticketbai.araba.eus"),
                [Environment.Production] = new Uri("https://ticketbai.araba.eus")
            }
        );
        QrBaseUrls = region.Match(
            Region.Gipuzkoa, _ => new Dictionary<Environment, Uri>
            {
                [Environment.Test] = new Uri("https://tbai.prep.gipuzkoa.eus"),
                [Environment.Production] = new Uri("https://tbai.egoitza.gipuzkoa.eus")
            },
            Region.Araba, _ => new Dictionary<Environment, Uri>
            {
                [Environment.Test] = new Uri("https://pruebas-ticketbai.araba.eus"),
                [Environment.Production] = new Uri("https://ticketbai.araba.eus")
            }
        );
        RelativeSendInvoiceUri = region.Match(
            Region.Gipuzkoa, _ => new Uri("sarrerak/alta/", UriKind.Relative),
            Region.Araba, _ => new Uri("TicketBAI/v1/facturas/", UriKind.Relative)
        );
        RelativeQrCodeUri = region.Match(
            Region.Gipuzkoa, _ => new Uri("qr/", UriKind.Relative),
            Region.Araba, _ => new Uri("tbai/qrtbai/", UriKind.Relative)
        );
        Encoding = Encoding.UTF8;
    }

    internal IDVersionTicketBaiType1 Version { get; }

    internal Dictionary<Environment, Uri> InvoiceBaseUrls { get; }

    internal Dictionary<Environment, Uri> QrBaseUrls { get; }

    internal Uri RelativeSendInvoiceUri { get; }

    internal Uri RelativeQrCodeUri { get; }

    internal Encoding Encoding { get; }

    internal Uri SendInvoiceUri(Environment environment)
    {
        return new Uri(InvoiceBaseUrls[environment], RelativeSendInvoiceUri);
    }
}