using System.Security.Cryptography.X509Certificates;
using Mews.Fiscalizations.Basque.Model;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Core.Xml;

namespace Mews.Fiscalizations.Basque
{
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

        public async Task<SendInvoiceResponse> SendInvoiceAsync(SendInvoiceRequest request)
        {
            var ticketBaiRequest = ModelToDtoConverter.Convert(request, ServiceInfo);
            var xmlDoc = XmlSerializer.Serialize(ticketBaiRequest, new XmlSerializationParameters(
                encoding: ServiceInfo.Encoding,
                namespaces: NonEmptyEnumerable.Create(new XmlNamespace("http://www.w3.org/2000/09/xmldsig#"))
            ));
            TicketBaiSignature.Sign(xmlDoc.OwnerDocument, Certificate, Region);

            var requestContent = new StringContent(xmlDoc.OuterXml, ServiceInfo.Encoding, "application/xml");
            var response = await HttpClient.PostAsync(ServiceInfo.SendInvoiceUri(Environment), requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var ticketBaiResponse = XmlSerializer.Deserialize<Dto.TicketBaiResponse>(responseContent);
            var header = request.Invoice.Header;
            var qrCodeUri = QrCodeUriGenerator.Generate(
                serviceInfo: ServiceInfo,
                environment: Environment,
                tbaiIdentifier: ticketBaiResponse.Salida.IdentificadorTBAI,
                invoiceSeries: header.Series,
                invoiceNumber: header.Number.Value,
                total: request.Invoice.InvoiceData.TotalAmount
            );
            var signatureValue = xmlDoc.GetElementsByTagName("SignatureValue")[0].InnerText;
            var trimmedSignature = String1To100.CreateUnsafe(signatureValue.Substring(0, Math.Min(signatureValue.Length, 100)));
            return DtoToModelConverter.Convert(ticketBaiResponse, qrCodeUri, xmlDoc.OuterXml, responseContent, trimmedSignature);
        }
    }
}