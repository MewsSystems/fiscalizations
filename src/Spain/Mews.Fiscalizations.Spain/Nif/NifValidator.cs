using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using FuncSharp;
using Mews.Fiscalizations.Core.Model;
using Mews.Fiscalizations.Spain.Communication;
using Mews.Fiscalizations.Spain.Model.Response;

namespace Mews.Fiscalizations.Spain.Nif;

public class NifValidator
{
    public NifValidator(X509Certificate certificate, TimeSpan httpTimeout)
    {
        var endpointUri = new Uri("https://www1.agenciatributaria.gob.es/wlpl/BURT-JDIT/ws/VNifV2SOAP");
        SoapClient = new SoapClient(endpointUri, certificate, httpTimeout);
        SoapClient.HttpRequestFinished += (sender, args) => HttpRequestFinished?.Invoke(this, args);
        SoapClient.XmlMessageSerialized += (sender, args) => XmlMessageSerialized?.Invoke(this, args);
    }

    public event EventHandler<HttpRequestFinishedEventArgs> HttpRequestFinished;

    public event EventHandler<XmlMessageSerializedEventArgs> XmlMessageSerialized;

    private SoapClient SoapClient { get; }

    public async Task<Try<Response, ErrorResult>> CheckNif(Request model)
    {
        var request = Convert(model);
        var response = await SoapClient.SendAsync<Entrada, Salida>(request);
        return response.Map(r => Convert(request, r));
    }

    private Response Convert(Entrada request, Salida response)
    {
        Check.Condition(request.Contribuyente.Count() == response.Contribuyente.Count(), "Invalid response. Request and response models count don't match.");

        return new Response(request.Contribuyente.Select(req =>
        {
            var nifResponse = response.Contribuyente.FirstOption(res => res.Nif == req.Nif);
            return nifResponse.Match(
                n =>
                {
                    var lowerCaseResult = n.Resultado?.ToLowerInvariant();
                    var result = lowerCaseResult.Match(
                        "identificado", _ => NifSearchResult.Found,
                        "no identificado", _ => NifSearchResult.NotFound,
                        "no procesado", _ => NifSearchResult.NotProcessed,
                        _ => NifSearchResult.Other
                    );
                    return new NifInfoResults(n.Nif, n.Nombre, result, n.Resultado);
                },
                _ => new NifInfoResults(req.Nif, req.Nombre, NifSearchResult.NotFoundBecauseNifModifiedByServer, nifResponse.Map(n => n.Resultado).GetOrNull())
            );
        }));
    }

    private Entrada Convert(Request value)
    {
        return new Entrada
        {
            Contribuyente = value.Entries.Select(e => new VNifV2EntContribuyente
            {
                Nombre = e.Name,
                Nif = e.TaxpayerNumber.TaxpayerNumber
            }).ToArray()
        };
    }
}