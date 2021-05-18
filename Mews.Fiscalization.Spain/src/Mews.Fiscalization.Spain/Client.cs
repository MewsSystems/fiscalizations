using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FuncSharp;
using Mews.Fiscalization.Spain.Communication;
using Mews.Fiscalization.Spain.Converters;
using Mews.Fiscalization.Spain.Dto.Requests;
using Mews.Fiscalization.Spain.Dto.Responses;
using Mews.Fiscalization.Spain.Model.Request;
using Mews.Fiscalization.Spain.Model.Response;

namespace Mews.Fiscalization.Spain
{
    public class Client
    {
        public Client(X509Certificate certificate, Environment environment, TimeSpan httpTimeout)
        {
            var domain = environment.Match(
                Environment.Test, _ => "www7.aeat.es",
                Environment.Production, _ => "www1.agenciatributaria.gob.es"
            );
            var endpointUri = new Uri($"https://{domain}/wlpl/SSII-FACT/ws/fe/SiiFactFEV1SOAP");
            SoapClient = new SoapClient(endpointUri, certificate, httpTimeout);
            SoapClient.HttpRequestFinished += (sender, args) => HttpRequestFinished?.Invoke(this, args);
            SoapClient.XmlMessageSerialized += (sender, args) => XmlMessageSerialized?.Invoke(this, args);
        }

        public event EventHandler<HttpRequestFinishedEventArgs> HttpRequestFinished;

        public event EventHandler<XmlMessageSerializedEventArgs> XmlMessageSerialized;

        private SoapClient SoapClient { get; }

        public async Task<ReceivedInvoices> SubmitInvoiceAsync(InvoicesToSubmit model)
        {
            var request = new ModelToDtoConverter().Convert(model);
            var response = await SoapClient.SendAsync<SubmitIssuedInvoicesRequest, SubmitIssuedInvoicesResponse>(request).ConfigureAwait(continueOnCapturedContext: false);
            return new DtoToModelConverter().Convert(response);
        }

        public async Task<ReceivedInvoices> SubmitSimplifiedInvoiceAsync(SimplifiedInvoicesToSubmit model)
        {
            var request = new ModelToDtoConverter().Convert(model);
            var response = await SoapClient.SendAsync<SubmitIssuedInvoicesRequest, SubmitIssuedInvoicesResponse>(request).ConfigureAwait(continueOnCapturedContext: false);
            return new DtoToModelConverter().Convert(response);
        }
    }
}
