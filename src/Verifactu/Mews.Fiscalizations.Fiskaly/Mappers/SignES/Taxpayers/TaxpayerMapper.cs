using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Taxpayers;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers;
using TaxpayerState = Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers.TaxpayerState;
using TaxpayerType = Mews.Fiscalizations.Fiskaly.Models.SignES.Taxpayers.TaxpayerType;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Taxpayers;

internal static class TaxpayerMapper
{
    public static ContentWrapper<CreateTaxpayerRequest> MapTaxpayerRequest(string legalName, string taxIdentifier, TaxpayerTerritory territory)
    {
        return new ContentWrapper<CreateTaxpayerRequest>
        {
            Content = new CreateTaxpayerRequest
            {
                Issuer = new TaxpayerIssuer
                {
                    LegalName = legalName,
                    TaxNumber = taxIdentifier
                },
                Territory = territory.MapTerritoryRequest()
            }
        };
    }
    
    public static Taxpayer MapTaxpayerResponse(this ContentWrapper<TaxpayerResponse> response)
    {
        return new Taxpayer(
            LegalName: response.Content.Issuer.LegalName,
            TaxIdentifier: response.Content.Issuer.TaxNumber,
            Territory: response.Content.Territory.MapTaxpayerTerritoryResponse(),
            Type: response.Content.Type.MapTaxpayerTypeResponse(),
            State: response.Content.State?.MapTaxpayerStateResponse() ?? TaxpayerState.Enabled
        );
    }

    private static Territory MapTerritoryRequest(this TaxpayerTerritory territory)
    {
        return territory switch
        {
            TaxpayerTerritory.Araba => Territory.ARABA,
            TaxpayerTerritory.Bizkaia => Territory.BIZKAIA,
            TaxpayerTerritory.Gipuzkoa => Territory.GIPUZKOA,
            TaxpayerTerritory.Navarre => Territory.NAVARRE,
            TaxpayerTerritory.CanaryIslands => Territory.CANARY_ISLANDS,
            TaxpayerTerritory.Ceuta => Territory.CEUTA,
            TaxpayerTerritory.Melilla => Territory.MELILLA,
            TaxpayerTerritory.SpainOther => Territory.SPAIN_OTHER,
            _ => throw new ArgumentOutOfRangeException(nameof(territory), territory, null)
        };
    }
    
    private static TaxpayerType MapTaxpayerTypeResponse(this DTOs.SignES.Taxpayers.TaxpayerType type)
    {
        return type switch
        {
            DTOs.SignES.Taxpayers.TaxpayerType.INDIVIDUAL => TaxpayerType.Individual,
            DTOs.SignES.Taxpayers.TaxpayerType.COMPANY => TaxpayerType.Company,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }

    private static TaxpayerState MapTaxpayerStateResponse(this DTOs.SignES.Taxpayers.TaxpayerState state)
    {
        return state switch
        {
            DTOs.SignES.Taxpayers.TaxpayerState.ENABLED => TaxpayerState.Enabled,
            DTOs.SignES.Taxpayers.TaxpayerState.DISABLED => TaxpayerState.Disabled,
            _ => throw new ArgumentOutOfRangeException(nameof(state), state, null)
        };
    }

    private static TaxpayerTerritory MapTaxpayerTerritoryResponse(this Territory territory)
    {
        return territory switch
        {
            Territory.ARABA => TaxpayerTerritory.Araba,
            Territory.BIZKAIA => TaxpayerTerritory.Bizkaia,
            Territory.GIPUZKOA => TaxpayerTerritory.Gipuzkoa,
            Territory.NAVARRE => TaxpayerTerritory.Navarre,
            Territory.CANARY_ISLANDS => TaxpayerTerritory.CanaryIslands,
            Territory.CEUTA => TaxpayerTerritory.Ceuta,
            Territory.MELILLA => TaxpayerTerritory.Melilla,
            Territory.SPAIN_OTHER => TaxpayerTerritory.SpainOther,
            _ => throw new ArgumentOutOfRangeException(nameof(territory), territory, null)
        };
    }
}