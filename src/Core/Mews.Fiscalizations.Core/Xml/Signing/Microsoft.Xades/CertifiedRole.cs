namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The CertifiedRoles element contains one or more wrapped attribute
/// certificates for the signer
/// </summary>
internal sealed class CertifiedRole : EncapsulatedPKIData
{
	/// <summary>
	/// Default constructor
	/// </summary>
	public CertifiedRole()
	{
		TagName = "CertifiedRole";
	}
}