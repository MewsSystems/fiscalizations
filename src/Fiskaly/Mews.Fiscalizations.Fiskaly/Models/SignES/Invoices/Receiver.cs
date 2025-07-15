namespace Mews.Fiscalizations.Fiskaly.Models.SignES.Invoices;

public sealed class Receiver
{
    public string LegalName { get; init; }
    public string Address { get; init; }
    public string PostalCode { get; init; }
    public string TaxIdentifier { get; init; }
    internal ReceiverType Type { get; init; }
    public string DocumentCountry { get; init; }
    public ForeignerDocumentType ForeignerDocumentType { get; init; }

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

public enum ReceiverType
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