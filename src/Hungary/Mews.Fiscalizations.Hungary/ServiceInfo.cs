using Mews.Fiscalizations.Core.Xml;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mews.Fiscalizations.Hungary;

public static class ServiceInfo
{
    public static int MaxInvoiceBatchSize { get; }

    internal static string Version { get; }

    internal static XmlNamespace XmlNamespace { get; }

    internal static Dictionary<NavEnvironment, Uri> BaseUrls { get; }

    internal static Uri RelativeServiceUrl { get; }

    internal static Encoding Encoding { get; }

    static ServiceInfo()
    {
        MaxInvoiceBatchSize = 100;
        Version = "3.0";
        XmlNamespace = new XmlNamespace("http://schemas.nav.gov.hu/OSA/3.0/api");
        BaseUrls = new Dictionary<NavEnvironment, Uri>
        {
            [NavEnvironment.Test] = new Uri("https://api-test.onlineszamla.nav.gov.hu"),
            [NavEnvironment.Live] = new Uri("https://api.onlineszamla.nav.gov.hu")
        };
        RelativeServiceUrl = new Uri("invoiceService/v3/", UriKind.Relative);
        Encoding = Encoding.UTF8;
    }
}