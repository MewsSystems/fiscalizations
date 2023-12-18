namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The EncapsulatedX509Certificate element is able to contain the
/// base64 encoding of a DER-encoded X.509 certificate
/// </summary>
internal sealed class EncapsulatedX509Certificate : EncapsulatedPKIData
{
	/// <summary>
	/// Default constructor
	/// </summary>
	public EncapsulatedX509Certificate()
	{
		TagName = "EncapsulatedX509Certificate";
	}
}