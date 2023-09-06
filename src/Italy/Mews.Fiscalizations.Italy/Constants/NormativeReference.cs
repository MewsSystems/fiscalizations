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

    public static string GetByInvoiceLineKind(TaxKind taxKind)
    {
        return taxKind.Match(
            TaxKind.ExcludedArticle15, _ => ExcludedArticle15,
            TaxKind.NotSubjectArticle7, _ => NotSubjectArticle7,
            TaxKind.NotSubjectOther, _ => NotSubjectOther,
            TaxKind.NonTaxableExports, _ => NonTaxableExports,
            TaxKind.NonTaxableIntraCommunity, _ => NonTaxableIntraCommunity,
            TaxKind.NonTaxableTransfersToSanMarino, _ => NonTaxableTransfersToSanMarino,
            TaxKind.NonTaxableOperationsAssimilatedToSalesOnExport, _ => NonTaxableOperationsAssimilatedToSalesOnExport,
            TaxKind.NonTaxableFollowingDeclarationsOfIntent, _ => NonTaxableFollowingDeclarationsOfIntent,
            TaxKind.NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation, _ => NonTaxableOtherOperationsThatDoNotContributeToTheCeilingFormation,
            TaxKind.Exempt, _ => Exempt,
            TaxKind.MarginScheme, _ => MarginScheme,
            _ => throw new InvalidOperationException("Unsupported invoice line kind.")
        );
    }
}