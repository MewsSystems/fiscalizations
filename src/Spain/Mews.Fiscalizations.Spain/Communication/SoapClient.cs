using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Core.Xml;
using Mews.Fiscalizations.Spain.Dto.Responses.SoapFault;
using Mews.Fiscalizations.Spain.Model.Response;
using FuncSharp;
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

        internal async Task<ResponseResult<TOut>> SendAsync<TIn, TOut>(TIn messageBodyObject)
            where TIn : class, new()
            where TOut : class, new()
        {
            var parameters = new XmlSerializationParameters(namespaces: GetSiiNameSpaces());
            var messageBodyXmlElement = XmlSerializer.Serialize(messageBodyObject, parameters);
            XmlMessageSerialized?.Invoke(this, new XmlMessageSerializedEventArgs(messageBodyXmlElement));

            var soapRequestMessage = new SoapMessage(messageBodyXmlElement);
            var xmlDocument = soapRequestMessage.GetXmlDocument();
            var xml = xmlDocument.OuterXml;

            var response = await GetResponseAsync(xml);
            var succesResult = Try.Catch<ResponseResult<TOut>, InvalidOperationException>(_ =>
            {
                var soapResponseBody = GetSoapBody(response);
                var deserializedMessage = XmlSerializer.Deserialize<TOut>(soapResponseBody.OuterXml);
                return ResponseResult.Success(deserializedMessage);
            });
            return succesResult.Match(s => s, _ => DeserializeSoapError<TOut>(response));
        }

        private static ResponseResult<TOut> DeserializeSoapError<TOut>(string response) where TOut : class, new()
        {
            try
            {
                var soapFault = XmlSerializer.Deserialize<SoapFaultResponse>(response);
                return ResponseResult.Error<TOut>(soapFault.Body.Fault.Code, soapFault.Body.Fault.Message);
            }
            catch (Exception e)
            {
                throw new InvalidOperationException($"Failed to parse response.", e);
            }
        }

        private async Task<string> GetResponseAsync(string body)
        {
            var requestContent = new StringContent(body, Encoding.UTF8, "application/x-www-form-urlencoded");

            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var response = await HttpClient.PostAsync(EndpointUri, requestContent))
            {
                var result = await response.Content.ReadAsStringAsync();

                stopwatch.Stop();
                var duration = stopwatch.ElapsedMilliseconds;
                HttpRequestFinished?.Invoke(this, new HttpRequestFinishedEventArgs(result, duration));

                return result;
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
