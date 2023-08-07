using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FuncSharp;
using Mews.Fiscalizations.Spain.Communication;
using Mews.Fiscalizations.Spain.Converters;
using Mews.Fiscalizations.Spain.Dto.Requests;
using Mews.Fiscalizations.Spain.Dto.Responses;
using Mews.Fiscalizations.Spain.Model.Request;
using Mews.Fiscalizations.Spain.Model.Response;

namespace Mews.Fiscalizations.Spain;

public class Client
{
    public Client(X509Certificate certificate, Environment environment, TimeSpan httpTimeout)
    {
        var domain = environment.Match(
            Environment.Test, _ => "prewww1.aeat.es",
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

    public async Task<Try<ReceivedInvoices, ErrorResult>> SubmitInvoiceAsync(InvoicesToSubmit model)
    {
        var request = new ModelToDtoConverter().Convert(model);
        var response = await SoapClient.SendAsync<SubmitIssuedInvoicesRequest, SubmitIssuedInvoicesResponse>(request);
        return response.Map(r => new DtoToModelConverter().Convert(r));
    }

    public async Task<Try<ReceivedInvoices, ErrorResult>> SubmitSimplifiedInvoiceAsync(SimplifiedInvoicesToSubmit model)
    {
        var request = new ModelToDtoConverter().Convert(model);
        var response = await SoapClient.SendAsync<SubmitIssuedInvoicesRequest, SubmitIssuedInvoicesResponse>(request);
        return response.Map(r => new DtoToModelConverter().Convert(r));
    }
}