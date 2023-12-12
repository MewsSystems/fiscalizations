namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class consist of a sequence of at least one OCSP Response. The
/// EncapsulatedOCSPValue element contains the base64 encoding of a
/// DER-encoded OCSP Response
/// </summary>
internal sealed class OCSPValue : EncapsulatedPKIData
{
	/// <summary>
	/// Default constructor
	/// </summary>
	public OCSPValue()
	{
		TagName = "EncapsulatedOCSPValue";
	}
}