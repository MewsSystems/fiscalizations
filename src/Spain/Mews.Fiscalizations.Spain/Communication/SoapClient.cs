using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Core.Xml;
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Mews.Fiscalizations.Spain.Communication
{
    internal class SoapClient
    {
        internal SoapClient(Uri endpointUri, X509Certificate certificate, TimeSpan httpTimeout)
        {
            EndpointUri = endpointUri;
            var requestHandler = new HttpClientHandler();
            requestHandler.ClientCertificates.Add(certificate);
            HttpClient = new HttpClient(requestHandler) { Timeout = httpTimeout };
        }

        internal event EventHandler<HttpRequestFinishedEventArgs> HttpRequestFinished;

        internal event EventHandler<XmlMessageSerializedEventArgs> XmlMessageSerialized;

        private Uri EndpointUri { get; }

        private HttpClient HttpClient { get; }

        internal async Task<TOut> SendAsync<TIn, TOut>(TIn messageBodyObject)
            where TIn : class, new()
            where TOut : class, new()
        {
            var parameters = new XmlSerializationParameters(namespaces: GetSiiNameSpaces());
            var messageBodyXmlElement = XmlSerializer.Serialize(messageBodyObject, parameters);
            XmlMessageSerialized?.Invoke(this, new XmlMessageSerializedEventArgs(messageBodyXmlElement));

            var soapMessage = new SoapMessage(messageBodyXmlElement);
            var xmlDocument = soapMessage.GetXmlDocument();
            var xml = xmlDocument.OuterXml;

            var response = await GetResponseAsync(xml);

            var soapBody = GetSoapBody(response);
            return XmlSerializer.Deserialize<TOut>(soapBody.OuterXml);
        }

        private async Task<string> GetResponseAsync(string body)
        {
            var requestContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var response = await HttpClient.PostAsync(EndpointUri, requestContent))
            {
                var result = await response.Content.ReadAsStringAsync();

                throw new Exception($"{response.StatusCode} {result}");
            }
        }

        private XmlElement GetSoapBody(string soapXmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapXmlString);

            var soapMessage = SoapMessage.FromSoapXml(xmlDocument);
            return soapMessage.Body.FirstChild as XmlElement;
        }

        private INonEmptyEnumerable<XmlNamespace> GetSiiNameSpaces()
        {
            return NonEmptyEnumerable.Create(
                new XmlNamespace("sii", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/SuministroInformacion.xsd"),
                new XmlNamespace("siiR", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaSuministro.xsd"),
                new XmlNamespace("siiLRC", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/ConsultaLR.xsd"),
                new XmlNamespace("siiLRRC", "https://www2.agenciatributaria.gob.es/static_files/common/internet/dep/aplicaciones/es/aeat/ssii/fact/ws/RespuestaConsultaLR.xsd")
            );
        }
    }
}
