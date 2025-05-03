using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Audit;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Audit;

internal static class AuditMapper
{
    public static SoftwareAuditData MapSoftwareAuditResponse(this SoftwareResponse response)
    {
        return new SoftwareAuditData(
            CompanyLegalName: response.Data.Company.LegalName,
            CompanyTaxIdentifier: response.Data.Company.TaxNumber,
            SoftwareName: response.Data.Name,
            License: response.Data.License,
            Version: response.Data.Version,
            ResponsibilityDeclaration: response.Data.ResponsibilityDeclaration ?? ""
        );
    }
}