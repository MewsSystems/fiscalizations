using System;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FuncSharp;
using Mews.Fiscalizations.Italy.Communication;
using Mews.Fiscalizations.Italy.Dto.Invoice;
using Mews.Fiscalizations.Italy.Dto.Receive;
using Mews.Fiscalizations.Italy.Http;

namespace Mews.Fiscalizations.Italy
{
    public class SdiClient
    {
        public SdiClient(Uri endpointUri, X509Certificate2 signatureCertificate, Func<HttpRequest, Task<HttpResponse>> httpClient)
        {
            SignatureCertificate = signatureCertificate;
            SoapClient = new SoapClient(endpointUri, httpClient);
        }

        private X509Certificate2 SignatureCertificate { get; }

        private SoapClient SoapClient { get; }

        public async Task<SdiResponse> SendAsync(ElectronicInvoice invoice)
        {
            var invoiceXmlDoc = XmlManipulator.Serialize(invoice);
            var signedInvoiceXmlDoc = XmlSigner.Sign(invoiceXmlDoc, SignatureCertificate);

            var signedInvoiceXml = signedInvoiceXmlDoc.OuterXml;
            var signedInvoiceBytes = Encoding.UTF8.GetBytes(signedInvoiceXml);

            var messageBody = new ReceiveFile
            {
                Content = signedInvoiceBytes,
                FileName = GetSignedInvoiceFileName(invoice)
            };

            var response = await SoapClient.SendAsync<ReceiveFile, ReceiveFileResponse>(messageBody, operation: "http://www.fatturapa.it/SdIRiceviFile/RiceviFile");

            if (response.ErrorSpecified)
            {
                return new SdiResponse(GetSdiError(response.Error));
            }

            return new SdiResponse(new SdiFileInfo(
                receivedUtc: response.ReceivedOn,
                sdiIdentifier: response.SdiIdentification
            ));
        }

        private SdiError GetSdiError(ReceiveFileError error)
        {
            return error.Match(
                ReceiveFileError.EmptyFile, _ => throw new InvalidOperationException("Attached file is empty."),
                ReceiveFileError.ServiceUnavailable, _ => SdiError.ServiceUnavailable,
                ReceiveFileError.UnauthorizedUser, _ => SdiError.UnauthorizedUser
            );
        }

        private string GetSignedInvoiceFileName(ElectronicInvoice invoice)
        {
            var transmitterId = invoice.Header.TransmissionData.TransmitterId;
            var fileIdentifier = $"{transmitterId.CountryCode}{transmitterId.TaxCode}_{invoice.Header.TransmissionData.SequentialNumber}".ToUpper();
            return $"{fileIdentifier}.xml";
        }
    }
}
