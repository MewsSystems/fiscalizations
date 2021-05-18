using System;
using System.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Xml;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Wsdl;
using Mews.Eet.Events;

namespace Mews.Eet.Communication
{
    public class SoapClient
    {
        public SoapClient(Uri endpointUri, Certificate certificate, TimeSpan httpTimeout, SignAlgorithm signAlgorithm = SignAlgorithm.Sha256, EetLogger logger = null)
        {
            HttpClient = new SoapHttpClient(endpointUri, httpTimeout, logger);
            Certificate = certificate;
            SignAlgorithm = signAlgorithm;
            XmlManipulator = new XmlManipulator();
            Logger = logger;
            HttpClient.HttpRequestFinished += (sender, args) => HttpRequestFinished?.Invoke(this, args);
        }

        public event EventHandler<HttpRequestFinishedEventArgs> HttpRequestFinished;

        public event EventHandler<XmlMessageSerializedEventArgs> XmlMessageSerialized;

        private SoapHttpClient HttpClient { get; }

        private Certificate Certificate { get; }

        private SignAlgorithm SignAlgorithm { get; }

        private XmlManipulator XmlManipulator { get; }

        private EetLogger Logger { get; }

        public async Task<TOut> SendAsync<TIn, TOut>(TIn messageBodyObject, string operation)
            where TIn : class, new()
            where TOut : class, new()
        {
            var messageBodyXmlElement = XmlManipulator.Serialize(messageBodyObject);
            var mesasgeBodyXmlString = messageBodyXmlElement.OuterXml;
            Logger?.Debug("Created XML document from DTOs.", new { XmlString = mesasgeBodyXmlString });
            XmlMessageSerialized?.Invoke(this, new XmlMessageSerializedEventArgs(messageBodyXmlElement, (messageBodyObject as SendRevenueXmlMessage)?.Data.BillNumber));

            var soapMessage = new SoapMessage(new SoapMessagePart(messageBodyXmlElement));
            var xmlDocument = Certificate == null ? soapMessage.GetXmlDocument() : soapMessage.GetSignedXmlDocument(Certificate, SignAlgorithm);

            var xml = xmlDocument.OuterXml;
            Logger?.Debug("Created signed XML.", new { SoapString = xml });

            var response = await HttpClient.SendAsync(xml, operation).ConfigureAwait(continueOnCapturedContext: false);

            Logger?.Debug("Received RAW response from EET servers.", new { HttpResponseBody = response });

            var soapBody = GetSoapBody(response);
            return XmlManipulator.Deserialize<TOut>(soapBody);
        }

        private XmlElement GetSoapBody(string soapXmlString)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(soapXmlString);

            var soapMessage = SoapMessage.FromSoapXml(xmlDocument);
            if (!soapMessage.VerifySignature())
            {
                throw new SecurityException("The SOAP message signature is not valid.");
            }
            return soapMessage.Body.XmlElement.FirstChild as XmlElement;
        }
    }
}
