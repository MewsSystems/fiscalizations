using Mews.Fiscalizations.Core.Model;

namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record ForeignReceiver(
    string Name,
    ForeignerDocumentType DocumentType,
    string DocumentNumber,
    Country DocumentCountry,
    string Address,
    string PostalCode
);

public enum ForeignerDocumentType
{
    TaxIdentifier = 0,
    Passport = 1,
    OfficialId = 2,
    ResidenceId = 3,
    Other = 4
}