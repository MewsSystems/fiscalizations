namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record SoftwareAuditData(
    string CompanyLegalName,
    string CompanyTaxIdentifier,
    string SoftwareName,
    string License,
    string Version,
    string ResponsibilityDeclaration
);