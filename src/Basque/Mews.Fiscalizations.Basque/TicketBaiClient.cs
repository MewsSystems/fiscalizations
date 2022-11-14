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
        public TicketBaiClient(X509Certificate2 certificate, Region region, Environment environment)
        {
            Certificate = certificate;
            Environment = environment;
            ServiceInfo = new ServiceInfo(region);

            var requestHandler = new HttpClientHandler();
            requestHandler.ClientCertificates.Add(certificate);
            HttpClient = new HttpClient(requestHandler);
        }

        private HttpClient HttpClient { get; }

        private X509Certificate2 Certificate { get; }

        private Environment Environment { get; }

        private ServiceInfo ServiceInfo { get; }

        public async Task<SendInvoiceResponse> SendInvoiceAsync(SendInvoiceRequest request)
        {
            var xmlDoc = GetSignedInvoiceDocument(request);
            var requestContent = new StringContent(xmlDoc.OuterXml, ServiceInfo.Encoding, "application/xml");

            var response = await HttpClient.PostAsync(ServiceInfo.SendInvoiceUri(Environment), requestContent);
            var responseContent = await response.Content.ReadAsStringAsync();
            var ticketBaiResponse = XmlSerializer.Deserialize<Dto.TicketBaiResponse>(responseContent);

            var signatureValue = xmlDoc.GetElementsByTagName("SignatureValue")[0].InnerText;
            var trimmedSignature = String1To100.CreateUnsafe(signatureValue.Substring(0, Math.Min(signatureValue.Length, 100)));
            return DtoToModelConverter.Convert(
                response: ticketBaiResponse,
                qrCodeUri: GetTbaiInvoiceData(request).QrCodeUri,
                xmlRequestContent: xmlDoc.OuterXml,
                xmlResponseContent: responseContent,
                signatureValue: trimmedSignature
            );
        }

        /// <summary>
        /// Generates the QR code and TBAI Identifier which must be displayed on the invoice without calling the API.
        /// To report the invoice to the gov authorities, SendInvoiceAsync must be used.
        /// </summary>
        /// <param name="request">Invoice request which will be mapped to Dto.TicketBai.</param>
        /// <returns></returns>
        public TbaiInvoiceData GetTbaiInvoiceData(SendInvoiceRequest request)
        {
            var signedRequest = GetSignedInvoiceDocument(request);
            var signatureValue = signedRequest.GetElementsByTagName("SignatureValue")[0].InnerText;
            var header = request.Invoice.Header;
            var tbaiIdentifier = GenerateTbaiIdentifier(signatureValue, request.Subject.Issuer.Nif.TaxpayerNumber, header.Issued);
            return new TbaiInvoiceData(tbaiIdentifier: tbaiIdentifier, qrCodeUri: QrCodeUriGenerator.Generate(
                serviceInfo: ServiceInfo,
                environment: Environment,
                tbaiIdentifier: tbaiIdentifier,
                invoiceSeries: header.Series,
                invoiceNumber: header.Number.Value,
                total: request.Invoice.InvoiceData.TotalAmount
            ));
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
            SignXml(xmlDoc.OwnerDocument);

            return xmlDoc;
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