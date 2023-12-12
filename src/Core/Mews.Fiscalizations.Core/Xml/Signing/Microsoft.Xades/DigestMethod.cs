using System.Security.Cryptography.Xml;
using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// DigestMethod indicates the digest algorithm
/// </summary>
internal sealed class DigestMethod
{
	/// <summary>
	/// Contains the digest algorithm
	/// </summary>
	public string Algorithm { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public DigestMethod()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Algorithm);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        Algorithm = xmlElement.GetAttribute("Algorithm");
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlDSigPrefix, "DigestMethod", SignedXml.XmlDsigNamespaceUrl);

		retVal.SetAttribute("Algorithm", Algorithm);

		return retVal;
	}
}