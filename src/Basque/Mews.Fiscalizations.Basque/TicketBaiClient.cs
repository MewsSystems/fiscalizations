using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Xml;
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
        public TicketBaiClient(X509Certificate2 certificate, Environment environment)
        {
            Environment = environment;
            Certificate = certificate;

            var requestHandler = new HttpClientHandler();
            requestHandler.ClientCertificates.Add(certificate);
            HttpClient = new HttpClient(requestHandler);
        }

        private HttpClient HttpClient { get; }

        private X509Certificate2 Certificate { get; }

        private Environment Environment { get; }

        public async Task<SendInvoiceResponse> SendInvoiceAsync(SendInvoiceRequest request)
        {
            var ticketBaiRequest = ModelToDtoConverter.Convert(request);
            var xmlDoc = XmlSerializer.Serialize(ticketBaiRequest, new XmlSerializationParameters(
                encoding: ServiceInfo.Encoding,
                namespaces: NonEmptyEnumerable.Create(new XmlNamespace("http://www.w3.org/2000/09/xmldsig#"))
            ));
            SignXml(xmlDoc.OwnerDocument);

            var requestContent = new StringContent(xmlDoc.OuterXml, ServiceInfo.Encoding, "application/xml");
            var response = await HttpClient.PostAsync(ServiceInfo.SendInvoiceUri(Environment), requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();

            var ticketBaiResponse = XmlSerializer.Deserialize<Dto.TicketBaiResponse>(responseContent);
            var header = request.Invoice.Header;
            var data = request.Invoice.InvoiceData;
            var qrCodeUri = QrCodeUriGenerator.Generate(
                environment: Environment,
                tbaiIdentifier: ticketBaiResponse.Salida.IdentificadorTBAI,
                invoiceSeries: header.Series,
                invoiceNumber: header.Number.Value,
                total: data.TotalAmount
            );
            return DtoToModelConverter.Convert(ticketBaiResponse, qrCodeUri, xmlDoc.OuterXml, responseContent);
        }

        private void SignXml(XmlDocument xmlDoc)
        {
            var keyInfo = new KeyInfo();
            keyInfo.AddClause(new KeyInfoX509Data(Certificate));
            var signedXml = new SignedXml(xmlDoc)
            {
                SigningKey = Certificate.GetRSAPrivateKey(),
                KeyInfo = keyInfo
            };

            var reference = new Reference(uri: "");
            var env = new XmlDsigEnvelopedSignatureTransform();
            reference.AddTransform(env);
            signedXml.AddReference(reference);
            signedXml.ComputeSignature();

            var xmlDigitalSignature = signedXml.GetXml();
            xmlDoc.DocumentElement.AppendChild(xmlDoc.ImportNode(xmlDigitalSignature, true));
        }
    }
}