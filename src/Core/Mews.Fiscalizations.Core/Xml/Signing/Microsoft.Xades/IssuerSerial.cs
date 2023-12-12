using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The element IssuerSerial contains the identifier of one of the
/// certificates referenced in the sequence
/// </summary>
internal sealed class IssuerSerial
{
	/// <summary>
	/// Name of the X509 certificate issuer
	/// </summary>
	public string X509IssuerName { get; set; }

	/// <summary>
	/// Serial number of the X509 certificate
	/// </summary>
	public string X509SerialNumber { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public IssuerSerial()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(X509IssuerName) || !string.IsNullOrEmpty(X509SerialNumber);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("ds", SignedXml.XmlDsigNamespaceUrl);

		var xmlNodeList = xmlElement.SelectNodes("ds:X509IssuerName", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("X509IssuerName missing");
		}
		X509IssuerName = xmlNodeList.Item(0).InnerText;

		xmlNodeList = xmlElement.SelectNodes("ds:X509SerialNumber", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("X509SerialNumber missing");
		}
		X509SerialNumber = xmlNodeList.Item(0).InnerText;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "IssuerSerial", XadesSignedXml.XadesNamespaceUri);
		retVal.SetAttribute("xmlns:ds", SignedXml.XmlDsigNamespaceUrl);

		var bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlDSigPrefix, "X509IssuerName", SignedXml.XmlDsigNamespaceUrl);
		bufferXmlElement.SetAttribute("xmlns:xades", XadesSignedXml.XadesNamespaceUri);
		bufferXmlElement.InnerText = X509IssuerName;
		retVal.AppendChild(bufferXmlElement);

		bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlDSigPrefix, "X509SerialNumber", SignedXml.XmlDsigNamespaceUrl);
		bufferXmlElement.SetAttribute("xmlns:xades", XadesSignedXml.XadesNamespaceUri);
		bufferXmlElement.InnerText = X509SerialNumber;

		retVal.AppendChild(bufferXmlElement);

		return retVal;
	}
}