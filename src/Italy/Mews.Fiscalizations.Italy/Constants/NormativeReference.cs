using Mews.Fiscalizations.Italy.Dto.Invoice;

namespace Mews.Fiscalizations.Italy.Constants;

public static class NormativeReference
{
    private const string ExcludedArticle15 = "escluse ex. art. 15";
    private const string NotSubjectArticle7 = "non soggette ad IVA ai sensi degli artt. da 7 a 7-septies del DPR 633/72";
    private const string NotSubjectOther = "non soggette – altri casi";
    private const string NonTaxableExports = "non imponibili – esportazioni";
    private const string NonTaxableIntraCommunity = "non imponibili – cessioni intracomunitarie";
    private const string NonTaxableTransfersToSanMarino = "non imponibili – cessioni verso San Marino";
    private const string NonTaxableOperationsAssimilatedToSalesOnExport = "non imponibili – operazioni assimilate alle cessioni all’esportazione";
    private const string NonTaxableFollowingDeclarationsOfIntent = "non imponibili – a seguito di dichiarazioni d’intento";
    private const string NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation = "non imponibili – altre operazioni che non concorrono alla formazione del plafond";
    private const string Exempt = "esenti";
    private const string MarginScheme = "regime del margine / IVA non esposta in fattura";
    private const string ReverseChargeScrapAndRecoveredMaterials = "inversione contabile – cessione di rottami e altri materiali di recupero";
    private const string ReverseChargeGoldAndSilver = "inversione contabile – cessione di oro e argento ai sensi della legge 7/2000 nonché di oreficeria usata ad OPO";
    private const string ReverseChargeConstructionSubcontracting = "inversione contabile – subappalto nel settore edile";
    private const string ReverseChargeBuildings = "inversione contabile – cessione di fabbricati";
    private const string ReverseChargeMobilePhones = "inversione contabile – cessione di telefoni cellulari";
    private const string ReverseChargeElectronicProducts = "inversione contabile – cessione di prodotti elettronici";
    private const string ReverseChargeConstructionAndRelatedSectors = "inversione contabile – prestazioni comparto edile e settori connessi";
    private const string ReverseChargeEnergySector = "inversione contabile – operazioni settore energetico";
    private const string ReverseChargeOther = "inversione contabile – altri casi";
    private const string VatPaidInOtherEuCountry = "IVA assolta in altro stato UE ex art. 7-octies lett. a, b, art. 74-sexies DPR 633/72";

    public static string GetByInvoiceLineKind(TaxKind taxKind)
    {
        return taxKind switch
        {
            TaxKind.ExcludedArticle15 => ExcludedArticle15,
            TaxKind.NotSubjectArticle7 => NotSubjectArticle7,
            TaxKind.NotSubjectOther => NotSubjectOther,
            TaxKind.NonTaxableExports => NonTaxableExports,
            TaxKind.NonTaxableIntraCommunity => NonTaxableIntraCommunity,
            TaxKind.NonTaxableTransfersToSanMarino => NonTaxableTransfersToSanMarino,
            TaxKind.NonTaxableOperationsAssimilatedToSalesOnExport => NonTaxableOperationsAssimilatedToSalesOnExport,
            TaxKind.NonTaxableFollowingDeclarationsOfIntent => NonTaxableFollowingDeclarationsOfIntent,
            TaxKind.NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation => NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation,
            TaxKind.Exempt => Exempt,
            TaxKind.MarginScheme => MarginScheme,
            TaxKind.ReverseChargeScrapAndRecoveredMaterials => ReverseChargeScrapAndRecoveredMaterials,
            TaxKind.ReverseChargeGoldAndSilver => ReverseChargeGoldAndSilver,
            TaxKind.ReverseChargeConstructionSubcontracting => ReverseChargeConstructionSubcontracting,
            TaxKind.ReverseChargeBuildings => ReverseChargeBuildings,
            TaxKind.ReverseChargeMobilePhones => ReverseChargeMobilePhones,
            TaxKind.ReverseChargeElectronicProducts => ReverseChargeElectronicProducts,
            TaxKind.ReverseChargeConstructionAndRelatedSectors => ReverseChargeConstructionAndRelatedSectors,
            TaxKind.ReverseChargeEnergySector => ReverseChargeEnergySector,
            TaxKind.ReverseChargeOther => ReverseChargeOther,
            TaxKind.VatPaidInOtherEuCountry => VatPaidInOtherEuCountry,
            _ => throw new InvalidOperationException("Unsupported invoice line kind.")
        };
    }
}