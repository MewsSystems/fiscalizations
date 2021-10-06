using System;
using System.Security;
using System.Threading.Tasks;
using System.Xml;
using Mews.Eet.Dto;
using Mews.Eet.Dto.Wsdl;
using Mews.Eet.Events;
using Mews.Fiscalizations.Core.Xml;

namespace Mews.Eet.Communication
{
    public class SoapClient
    {
        public SoapClient(Uri endpointUri, Certificate certificate, TimeSpan httpTimeout, SignAlgorithm signAlgorithm = SignAlgorithm.Sha256, EetLogger logger = null)
        {
            HttpClient = new SoapHttpClient(endpointUri, httpTimeout, logger);
            Certificate = certificate;
            SignAlgorithm = signAlgorithm;
            Logger = logger;
            HttpClient.HttpRequestFinished += (sender, args) => HttpRequestFinished?.Invoke(this, args);
        }

        public event EventHandler<HttpRequestFinishedEventArgs> HttpRequestFinished;

        public event EventHandler<XmlMessageSerializedEventArgs> XmlMessageSerialized;

        private SoapHttpClient HttpClient { get; }

        private Certificate Certificate { get; }

        private SignAlgorithm SignAlgorithm { get; }

        private EetLogger Logger { get; }

        public async Task<TOut> SendAsync<TIn, TOut>(TIn messageBodyObject, string operation)
            where TIn : class, new()
            where TOut : class, new()
        {
            var messageBodyXmlElement = XmlSerializer.Serialize(messageBodyObject).DocumentElement;
            var mesasgeBodyXmlString = messageBodyXmlElement.OuterXml;
            Logger?.Debug("Created XML document from DTOs.", new { XmlString = mesasgeBodyXmlString });
            XmlMessageSerialized?.Invoke(this, new XmlMessageSerializedEventArgs(messageBodyXmlElement, (messageBodyObject as SendRevenueXmlMessage)?.Data.BillNumber));

            var soapMessage = new SoapMessage(new SoapMessagePart(messageBodyXmlElement));
            var xmlDocument = Certificate == null ? soapMessage.GetXmlDocument() : soapMessage.GetSignedXmlDocument(Certificate, SignAlgorithm);

            var xml = xmlDocument.OuterXml;
            Logger?.Debug("Created signed XML.", new { SoapString = xml });

            var response = await HttpClient.SendAsync(xml, operation);

            Logger?.Debug("Received RAW response from EET servers.", new { HttpResponseBody = response });

            var soapBody = GetSoapBody(response);
            return XmlSerializer.Deserialize<TOut>(soapBody.OuterXml);
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
