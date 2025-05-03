namespace Mews.Fiscalizations.Fiskaly.Models;

public sealed record Signer(Guid Id, SignerCertificate Certificate);

public sealed record SignerCertificate(string SerialNumber, string Data, DateTime Expiration);