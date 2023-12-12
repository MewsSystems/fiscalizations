using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The NoticeRef element names an organization and identifies by
/// numbers a group of textual statements prepared by that organization,
/// so that the application could get the explicit notices from a notices file.
/// </summary>
internal sealed class NoticeRef
{
	/// <summary>
	/// Organization issuing the signature policy
	/// </summary>
	public string Organization { get; set; }

	/// <summary>
	/// Numerical identification of textual statements prepared by the organization,
	/// so that the application can get the explicit notices from a notices file.
	/// </summary>
	public NoticeNumbers NoticeNumbers { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public NoticeRef()
	{
		NoticeNumbers = new NoticeNumbers();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Organization) || NoticeNumbers != null && NoticeNumbers.HasChanged();
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

		var xmlNodeList = xmlElement.SelectNodes("xsd:Organization", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("Organization missing");
		}
		Organization = xmlNodeList.Item(0).InnerText;

		xmlNodeList = xmlElement.SelectNodes("xsd:NoticeNumbers", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("NoticeNumbers missing");
		}
		NoticeNumbers = new NoticeNumbers();
		NoticeNumbers.LoadXml((XmlElement)xmlNodeList.Item(0));
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("NoticeRef", XadesSignedXml.XadesNamespaceUri);

		if (Organization == null)
		{
			throw new CryptographicException("Organization can't be null");
		}
		var bufferXmlElement = creationXmlDocument.CreateElement("Organization", XadesSignedXml.XadesNamespaceUri);
		bufferXmlElement.InnerText = Organization;
		retVal.AppendChild(bufferXmlElement);

		if (NoticeNumbers == null)
		{
			throw new CryptographicException("NoticeNumbers can't be null");
		}
		retVal.AppendChild(creationXmlDocument.ImportNode(NoticeNumbers.GetXml(), true));

		return retVal;
	}
}