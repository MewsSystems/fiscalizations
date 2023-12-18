using System.Xml;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class represents the unique object of a XAdES signature that
/// contains all XAdES information
/// </summary>
internal sealed class XadesObject
{
	/// <summary>
	/// Id attribute of the XAdES object
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// The QualifyingProperties element acts as a container element for
	/// all the qualifying information that should be added to an XML
	/// signature.
	/// </summary>
	public QualifyingProperties QualifyingProperties { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public XadesObject()
	{
		QualifyingProperties = new QualifyingProperties();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Id) || QualifyingProperties != null && QualifyingProperties.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	/// <param name="counterSignedXmlElement">Element containing parent signature (needed if there are counter signatures)</param>
	public void LoadXml(XmlElement xmlElement, XmlElement counterSignedXmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        Id = xmlElement.HasAttribute("Id") ? xmlElement.GetAttribute("Id") : "";

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xades", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xades:QualifyingProperties", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("QualifyingProperties missing");
		}
		QualifyingProperties = new QualifyingProperties();
		QualifyingProperties.LoadXml((XmlElement)xmlNodeList.Item(0), counterSignedXmlElement);

		xmlNodeList = xmlElement.SelectNodes("xades:QualifyingPropertiesReference", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			throw new CryptographicException("Current implementation can't handle QualifyingPropertiesReference element");
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("ds", "Object", SignedXml.XmlDsigNamespaceUrl);
		if (!string.IsNullOrEmpty(Id))
		{
			retVal.SetAttribute("Id", Id);
		}

		if (QualifyingProperties != null && QualifyingProperties.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(QualifyingProperties.GetXml(), true));
		}

		return retVal;
	}
}