using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class identifies one OCSP response
/// </summary>
internal sealed class OCSPRef
{
	/// <summary>
	/// Identification of one OCSP response
	/// </summary>
	public OCSPIdentifier OCSPIdentifier { get; set; }

	/// <summary>
	/// The digest computed on the DER encoded OCSP response, since it may be
	/// needed to differentiate between two OCSP responses by the same server
	/// with their "ProducedAt" fields within the same second.
	/// </summary>
	public DigestAlgAndValueType CertDigest { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OCSPRef()
	{
		OCSPIdentifier = new OCSPIdentifier();
		CertDigest = new DigestAlgAndValueType("DigestAlgAndValue");
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return OCSPIdentifier != null && OCSPIdentifier.HasChanged() || CertDigest != null && CertDigest.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:OCSPIdentifier", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("OCSPIdentifier missing");
		}
		OCSPIdentifier = new OCSPIdentifier();
		OCSPIdentifier.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:DigestAlgAndValue", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			CertDigest = null;
		}
		else
		{
			CertDigest = new DigestAlgAndValueType("DigestAlgAndValue");
			CertDigest.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "OCSPRef", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		if (OCSPIdentifier != null && OCSPIdentifier.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(OCSPIdentifier.GetXml(), true));
		}
		else
		{
			throw new CryptographicException("OCSPIdentifier element missing in OCSPRef");
		}

		if (CertDigest != null && CertDigest.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CertDigest.GetXml(), true));
		}

		return retVal;
	}
}