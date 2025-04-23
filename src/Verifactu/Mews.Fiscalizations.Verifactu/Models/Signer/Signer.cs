namespace Mews.Fiscalizations.Verifactu.Models;

public sealed record Signer(Guid Id, SignerCertificate Certificate);

public sealed record SignerCertificate(string SerialNumber, string Data, DateTime Expiration);