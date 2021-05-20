using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Mews.Fiscalization.Italy.Http;

namespace Mews.Fiscalization.Italy.Communication
{
    class SoapClient
    {
        public SoapClient(Uri endpointUri, Func<HttpRequest, Task<HttpResponse>> httpClient)
        {
            EndpointUri = endpointUri;
            HttpClient = httpClient;
        }

        private Uri EndpointUri { get; }

        private Func<HttpRequest, Task<HttpResponse>> HttpClient { get; }

        public async Task<TOut> SendAsync<TIn, TOut>(TIn messageBodyObject, string operation)
            where TIn : class, new()
            where TOut : class, new()
        {
            var messageBodyXmlElement = XmlManipulator.Serialize(messageBodyObject).DocumentElement;

            var soapMessage = new SoapMessage(new SoapMessagePart(messageBodyXmlElement));
            var xmlDocument = soapMessage.GetXmlDocument();

            var xml = xmlDocument.OuterXml;
            var httpRequest = GetHttpRequest(operation, xml);

            var response = await HttpClient(httpRequest).ConfigureAwait(continueOnCapturedContext: false);

            var soapBody = GetSoapBody(response.Content.Value);
            return XmlManipulator.Deserialize<TOut>(soapBody);
        }

        private HttpRequest GetHttpRequest(string operation, string messageXml)
        {
            return new HttpRequest(
                uri: EndpointUri,
                method: HttpMethod.Post,
                content: new HttpContent(
                    value: messageXml,
                    encoding: Encoding.UTF8,
                    mimeType: "text/xml"
               ),
                headers: new Dictionary<string, string>
                {
                    ["SOAPAction"] = operation
                }
            );
        }

        private XmlElement GetSoapBody(string soapXmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapXmlString);

            var soapMessage = SoapMessage.FromSoapXml(xmlDocument);
            return soapMessage.Body.XmlElement.FirstChild as XmlElement;
        }
    }
}
