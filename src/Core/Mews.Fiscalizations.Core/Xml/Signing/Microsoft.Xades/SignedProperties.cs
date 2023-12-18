using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The SignedProperties element contains a number of properties that are
/// collectively signed by the XMLDSIG signature
/// </summary>
internal sealed class SignedProperties
{
	/// <summary>
	/// Default value for the SignedProperties Id attribute
	/// </summary>
	public const string DefaultSignedPropertiesId = "SignedPropertiesId";

	/// <summary>
	/// This Id is used to be able to point the signature reference to this
	/// element.  It is initialized by default.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// The properties that qualify the signature itself or the signer are
	/// included as content of the SignedSignatureProperties element
	/// </summary>
	public SignedSignatureProperties SignedSignatureProperties { get; set; }

	/// <summary>
	/// The SignedDataObjectProperties element contains properties that qualify
	/// some of the signed data objects
	/// </summary>
	public SignedDataObjectProperties SignedDataObjectProperties { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public SignedProperties()
	{
		Id = DefaultSignedPropertiesId;
		SignedSignatureProperties = new SignedSignatureProperties();
		SignedDataObjectProperties = new SignedDataObjectProperties();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Id)
		       || SignedSignatureProperties != null
		       && SignedSignatureProperties.HasChanged()
		       || SignedDataObjectProperties != null
		       && SignedDataObjectProperties.HasChanged();
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);
        Id = xmlElement.HasAttribute("Id") ? xmlElement.GetAttribute("Id") : "";

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:SignedSignatureProperties", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("SignedSignatureProperties missing");
		}
		SignedSignatureProperties = new SignedSignatureProperties();
		SignedSignatureProperties.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:SignedDataObjectProperties", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			SignedDataObjectProperties = new SignedDataObjectProperties();
			SignedDataObjectProperties.LoadXml((XmlElement)xmlNodeList.Item(0));
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SignedProperties", XadesSignedXml.XadesNamespaceUri);
		if (!string.IsNullOrEmpty(Id))
		{
			retVal.SetAttribute("Id", Id);
		}

		if (SignedSignatureProperties != null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SignedSignatureProperties.GetXml(), true));
		}
		else
		{
			throw new CryptographicException("SignedSignatureProperties should not be null");
		}

		if (SignedDataObjectProperties != null && SignedDataObjectProperties.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SignedDataObjectProperties.GetXml(), true));
		}

		return retVal;
	}
}