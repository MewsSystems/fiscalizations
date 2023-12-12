using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains information about a Certificate Revocation List (CRL)
/// </summary>
internal sealed class CRLRef
{
	/// <summary>
	/// The digest of the entire DER encoded
	/// </summary>
	public DigestAlgAndValueType CertDigest { get; set; }

	/// <summary>
	/// CRLIdentifier is a set of data including the issuer, the time when
	/// the CRL was issued and optionally the number of the CRL.
	/// The Identifier element can be dropped if the CRL could be inferred
	/// from other information.
	/// </summary>
	public CRLIdentifier CRLIdentifier { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public CRLRef()
	{
		CertDigest = new DigestAlgAndValueType("DigestAlgAndValue");
		CRLIdentifier = new CRLIdentifier();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CertDigest != null && CertDigest.HasChanged() || CRLIdentifier != null && CRLIdentifier.HasChanged();
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

		var xmlNodeList = xmlElement.SelectNodes("xsd:DigestAlgAndValue", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("DigestAlgAndValue missing");
		}
		CertDigest = new DigestAlgAndValueType("DigestAlgAndValue");
		CertDigest.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:CRLIdentifier", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			CRLIdentifier = null;
		}
		else
		{
			CRLIdentifier = new CRLIdentifier();
			CRLIdentifier.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "CRLRef", XadesSignedXml.XadesNamespaceUri);

		if (CertDigest is not null && CertDigest.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CertDigest.GetXml(), true));
		}
		else
		{
			throw new CryptographicException("DigestAlgAndValue element missing in CRLRef");
		}

		if (CRLIdentifier != null && CRLIdentifier.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CRLIdentifier.GetXml(), true));
		}

		return retVal;
	}
}