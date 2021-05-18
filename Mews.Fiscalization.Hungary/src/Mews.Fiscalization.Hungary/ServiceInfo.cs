using System;
using System.Collections.Generic;

namespace Mews.Fiscalization.Hungary
{
    public static class ServiceInfo
    {
        public static int MaxInvoiceBatchSize { get; }

        internal static string Version  { get; }

        internal static string XmlNamespace { get; }

        internal static Dictionary<NavEnvironment, Uri> BaseUrls { get; }

        internal static Uri RelativeServiceUrl { get; }

        static ServiceInfo()
        {
            MaxInvoiceBatchSize = 100;
            Version = "2.0";
            XmlNamespace = "http://schemas.nav.gov.hu/OSA/2.0/api";
            BaseUrls = new Dictionary<NavEnvironment, Uri>
            {
                [NavEnvironment.Test] = new Uri("https://api-test.onlineszamla.nav.gov.hu"),
                [NavEnvironment.Live] = new Uri("https://api.onlineszamla.nav.gov.hu")
            };
            RelativeServiceUrl = new Uri("invoiceService/v2/", UriKind.Relative);
        }
    }
}