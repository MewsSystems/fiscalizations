namespace Mews.Fiscalizations.Core.Xml.Signing.Signature.Parameters;

public sealed record DataFormat(string MimeType, string Description = null, string TypeIdentifier = null);