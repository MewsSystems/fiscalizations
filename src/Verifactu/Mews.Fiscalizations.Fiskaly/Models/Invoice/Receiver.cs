namespace Mews.Fiscalizations.Fiskaly.Models;

public sealed class Receiver
{
    public string LegalName { get; private init; }
    public string Address { get; private init; }
    public string PostalCode { get; private init; }
    public string TaxIdentifier { get; private init; }
    internal ReceiverType Type { get; private init; }
    public string DocumentCountry { get; private init; }
    public ForeignerDocumentType ForeignerDocumentType { get; private init; }

    public static Receiver CreateLocal(string name, string taxIdentifier, string address, string postalCode)
    {
        return new Receiver
        {
            Type = ReceiverType.Local,
            LegalName = name,
            Address = address,
            PostalCode = postalCode,
            TaxIdentifier = taxIdentifier
        };
    }

    public static Receiver CreateForeign(string name, ForeignerDocumentType type, string number, string documentCountry, string address, string postalCode)
    {
        return new Receiver
        {
            Type = ReceiverType.Foreign,
            LegalName = name,
            Address = address,
            PostalCode = postalCode,
            TaxIdentifier = number,
            DocumentCountry = documentCountry,
            ForeignerDocumentType = type
        };
    }
}

internal enum ReceiverType
{
    Local,
    Foreign
}

public enum ForeignerDocumentType
{
    TaxIdentifier = 0,
    Passport = 1,
    OfficialId = 2,
    ResidenceId = 3,
    Other = 4
}