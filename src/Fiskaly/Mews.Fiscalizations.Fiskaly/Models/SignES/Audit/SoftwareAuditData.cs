namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Audit;

public sealed record SoftwareAuditData(
    string CompanyLegalName,
    string CompanyTaxIdentifier,
    string SoftwareName,
    string License,
    string Version,
    string ResponsibilityDeclaration
);