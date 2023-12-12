using System.Xml;
using System.Security.Cryptography;


namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The QualifyingProperties element acts as a container element for
/// all the qualifying information that should be added to an XML
/// signature
/// </summary>
internal sealed class QualifyingProperties
{
	/// <summary>
	/// The optional Id attribute can be used to make a reference to the
	/// QualifyingProperties container.
	/// </summary>
	public string Id { get; set; }

	/// <summary>
	/// The mandatory Target attribute refers to the XML signature with which the
	/// qualifying properties are associated.
	/// </summary>
	public string Target { get; set; }

	/// <summary>
	/// The SignedProperties element contains a number of properties that are
	/// collectively signed by the XMLDSIG signature
	/// </summary>
	public SignedProperties SignedProperties { get; set; }

	/// <summary>
	/// The UnsignedProperties element contains a number of properties that are
	/// not signed by the XMLDSIG signature
	/// </summary>
	public UnsignedProperties UnsignedProperties { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public QualifyingProperties()
	{
		SignedProperties = new SignedProperties();
		UnsignedProperties = new UnsignedProperties();
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(Id)
		       || !string.IsNullOrEmpty(Target)
		       || SignedProperties != null
		       && SignedProperties.HasChanged()
		       || UnsignedProperties != null
		       && UnsignedProperties.HasChanged();
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

		if (xmlElement.HasAttribute("Target"))
		{
			Target = xmlElement.GetAttribute("Target");
		}
		else
		{
			Target = "";
			throw new CryptographicException("Target attribute missing");
		}

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:SignedProperties", xmlNamespaceManager);
		if (xmlNodeList.Count == 0)
		{
			throw new CryptographicException("SignedProperties missing");
		}
		SignedProperties = new SignedProperties();
		SignedProperties.LoadXml((XmlElement)xmlNodeList.Item(0));

		xmlNodeList = xmlElement.SelectNodes("xsd:UnsignedProperties", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			UnsignedProperties = new UnsignedProperties();
			UnsignedProperties.LoadXml((XmlElement)xmlNodeList.Item(0), counterSignedXmlElement);
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "QualifyingProperties", XadesSignedXml.XadesNamespaceUri);
          
		if (!string.IsNullOrEmpty(Id))
		{
			retVal.SetAttribute("Id", Id);
		}

		if (!string.IsNullOrEmpty(Target))
		{
			retVal.SetAttribute("Target", Target);
		}
		else
		{
			throw new CryptographicException("QualifyingProperties Target attribute has no value");
		}

		if (SignedProperties != null && SignedProperties.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(SignedProperties.GetXml(), true));
		}
		if (UnsignedProperties != null && UnsignedProperties.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(UnsignedProperties.GetXml(), true));
		}

		return retVal;
	}
}