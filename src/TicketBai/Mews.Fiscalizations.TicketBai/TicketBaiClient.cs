using FuncSharp;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Mews.Fiscalizations.TicketBai.Model;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mews.Fiscalizations.TicketBai
{
    public sealed class TicketBaiClient
    {
        public TicketBaiClient(X509Certificate2 certificate, Environment environment)
        {
            Environment = environment;
            SendInvoiceUri = environment.Match(
                Environment.Production, _ => "https://tbai-z.egoitza.gipuzkoa.eus/sarrerak/alta",
                Environment.Test, _ => "https://tbai-z.prep.gipuzkoa.eus/sarrerak/alta/"
            );
            Certificate = certificate;

            var requestHandler = new HttpClientHandler();
            requestHandler.ClientCertificates.Add(certificate);
            HttpClient = new HttpClient(requestHandler);
        }

        private string SendInvoiceUri { get; }

        private HttpClient HttpClient { get; }

        private X509Certificate2 Certificate { get; }

        private Environment Environment { get; }

        // TODO: Return ITry and handle error responses?.
        public async Task<SendInvoiceResponse> SendInvoiceAsync(SendInvoiceRequest request)
        {
            var ticketBaiRequest = ModelToDtoConverter.Convert(request);
            var xmlDoc = new XmlDocument();

            using (var memStm = new MemoryStream())
            {
                var serializer = new XmlSerializer(typeof(Dto.TicketBai));
                serializer.Serialize(memStm, ticketBaiRequest);

                memStm.Position = 0;

                using (var xtr = new StreamReader(memStm, Encoding.UTF8))
                {
                    xmlDoc.PreserveWhitespace = true;
                    xmlDoc.Load(xtr);
                }
            }

            var attr = xmlDoc.CreateAttribute("xmlns:ds");
            attr.Value = "http://www.w3.org/2000/09/xmldsig#";
            xmlDoc.DocumentElement.Attributes.Append(attr);
            SignXml(xmlDoc);

            var requestContent = new StringContent(xmlDoc.OuterXml, Encoding.UTF8, "application/xml");
            var response = await HttpClient.PostAsync(SendInvoiceUri, requestContent);
            var content = await response.Content.ReadAsStringAsync();
            var ticketBaiResponse = Core.Xml.XmlSerializer.Deserialize<Dto.TicketBaiResponse>(content);

            var header = request.Invoice.Header;
            var data = request.Invoice.InvoiceData;
            var qrCodeUri = CRC8.Calculate(
                tbaiIdentifier: ticketBaiResponse.Salida.IdentificadorTBAI,
                invoiceSeries: header.Series,
                invoiceNumber: header.Number.Value,
                total: data.TotalAmount,
                environment: Environment
            );
            return DtoToModelConverter.Convert(ticketBaiResponse, qrCodeUri, xmlDoc.InnerXml);
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