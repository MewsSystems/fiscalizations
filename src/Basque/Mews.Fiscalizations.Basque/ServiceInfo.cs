using Mews.Fiscalizations.Basque.Dto;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Basque
{
    public static class ServiceInfo
    {
        internal static IDVersionTicketBaiType1 Version { get; }

        internal static Dictionary<Environment, Uri> InvoiceBaseUrls { get; }

        internal static Dictionary<Environment, Uri> QrBaseUrls { get; }

        internal static Uri RelativeSendInvoiceUri { get; }

        internal static Uri RelativeQrCodeUri { get; }

        internal static Encoding Encoding { get; }

        static ServiceInfo()
        {
            Version = IDVersionTicketBaiType1.Item12;
            InvoiceBaseUrls = new Dictionary<Environment, Uri>
            {
                [Environment.Test] = new Uri("https://tbai-z.prep.gipuzkoa.eus"),
                [Environment.Production] = new Uri("https://tbai-z.egoitza.gipuzkoa.eus")
            };
            QrBaseUrls = new Dictionary<Environment, Uri>
            {
                [Environment.Test] = new Uri("https://tbai.prep.gipuzkoa.eus"),
                [Environment.Production] = new Uri("https://tbai.egoitza.gipuzkoa.eus")
            };
            RelativeSendInvoiceUri = new Uri("sarrerak/alta/", UriKind.Relative);
            RelativeQrCodeUri = new Uri("qr/", UriKind.Relative);
            Encoding = Encoding.UTF8;
        }

        internal static Uri SendInvoiceUri(Environment environment)
        {
            return new Uri(InvoiceBaseUrls[environment], RelativeSendInvoiceUri);
        }
    }
}