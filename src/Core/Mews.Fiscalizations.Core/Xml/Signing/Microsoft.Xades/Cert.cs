using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains certificate identification information
/// </summary>
internal sealed class Cert
{
	/// <summary>
	/// The element CertDigest contains the digest of one of the
	/// certificates referenced in the sequence
	/// </summary>
	public DigestAlgAndValueType CertDigest { get; set; }

	/// <summary>
	/// The element IssuerSerial contains the identifier of one of the
	/// certificates referenced in the sequence. Should the
	/// X509IssuerSerial element appear in the signature to denote the same
	/// certificate, its value MUST be consistent with the corresponding
	/// IssuerSerial element.
	/// </summary>
	public IssuerSerial IssuerSerial { get; set; }

	/// <summary>
	/// Element's URI
	/// </summary>
	public string URI { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public Cert()
	{
		CertDigest = new DigestAlgAndValueType("CertDigest");
		IssuerSerial = new IssuerSerial();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return CertDigest is not null && CertDigest.HasChanged() || IssuerSerial is not null && IssuerSerial.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        if (xmlElement.HasAttribute("URI"))
		{
			URI = xmlElement.GetAttribute("URI");
		}

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:CertDigest", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("CertDigest missing");
		}
		CertDigest = new DigestAlgAndValueType("CertDigest");
		CertDigest.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:IssuerSerial", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("IssuerSerial missing");
		}
		IssuerSerial = new IssuerSerial();
		IssuerSerial.LoadXml((XmlElement)xmlNodeList.Item(0));
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "Cert", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		if (!string.IsNullOrEmpty(URI))
		{
			retVal.SetAttribute("URI", URI);
		}

		if (CertDigest is not null && CertDigest.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(CertDigest.GetXml(), true));
		}

		if (IssuerSerial is not null && IssuerSerial.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(IssuerSerial.GetXml(), true));
		}

		return retVal;
	}
}