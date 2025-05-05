using Mews.Fiscalizations.Fiskaly.DTOs.SignES;
using Mews.Fiscalizations.Fiskaly.DTOs.SignES.Audit;
using Mews.Fiscalizations.Fiskaly.Models.SignES.Audit;

namespace Mews.Fiscalizations.Fiskaly.Mappers.SignES.Audit;

internal static class AuditMapper
{
    public static SoftwareAuditData MapSoftwareAuditResponse(this ContentWrapper<SoftwareDataResponse> response)
    {
        return new SoftwareAuditData(
            CompanyLegalName: response.Content.Company.LegalName,
            CompanyTaxIdentifier: response.Content.Company.TaxNumber,
            SoftwareName: response.Content.Name,
            License: response.Content.License,
            Version: response.Content.Version,
            ResponsibilityDeclaration: response.Content.ResponsibilityDeclaration ?? ""
        );
    }
}