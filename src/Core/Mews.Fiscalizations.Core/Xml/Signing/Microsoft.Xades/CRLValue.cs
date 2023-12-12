namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class consist of a sequence of at least one Certificate Revocation
/// List. Each EncapsulatedCRLValue will contain the base64 encoding of a
/// DER-encoded X509 CRL.
/// </summary>
internal sealed class CRLValue : EncapsulatedPKIData
{
	/// <summary>
	/// Default constructor
	/// </summary>
	public CRLValue()
	{
		TagName = "EncapsulatedCRLValue";
	}
}