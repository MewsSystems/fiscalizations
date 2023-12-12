using System.Xml;
using System.Security.Cryptography;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// The DataObjectFormat element provides information that describes the
/// format of the signed data object. This element must be present when it
/// is mandatory to present the signed data object to human users on
/// verification.
/// This is a signed property that qualifies one specific signed data
/// object. In consequence, a XAdES signature may contain more than one
/// DataObjectFormat elements, each one qualifying one signed data object.
/// </summary>
internal sealed class DataObjectFormat
{
	/// <summary>
	/// The mandatory ObjectReference attribute refers to the Reference element
	/// of the signature corresponding with the data object qualified by this
	/// property.
	/// </summary>
	public string ObjectReferenceAttribute { get; set; }

	/// <summary>
	/// Textual information related to the signed data object
	/// </summary>
	public string Description { get; set; }

	/// <summary>
	/// An identifier indicating the type of the signed data object
	/// </summary>
	public ObjectIdentifier ObjectIdentifier { get; set; }

	/// <summary>
	/// An indication of the MIME type of the signed data object
	/// </summary>
	public string MimeType { get; set; }

	/// <summary>
	/// An indication of the encoding format of the signed data object
	/// </summary>
	public string Encoding { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public DataObjectFormat()
	{
		ObjectIdentifier = new ObjectIdentifier("ObjectIdentifier");
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(ObjectReferenceAttribute)
		       || !string.IsNullOrEmpty(Description)
		       || ObjectIdentifier != null && ObjectIdentifier.HasChanged()
		       || !string.IsNullOrEmpty(MimeType)
		       || !string.IsNullOrEmpty(Encoding);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        if (xmlElement.HasAttribute("ObjectReference"))
		{
			ObjectReferenceAttribute = xmlElement.GetAttribute("ObjectReference");
		}
		else
		{
			ObjectReferenceAttribute = "";
			throw new CryptographicException("ObjectReference attribute missing");
		}

		var xmlNamespaceManager = new XmlNamespaceManager(xmlElement.OwnerDocument.NameTable);
		xmlNamespaceManager.AddNamespace("xsd", XadesSignedXml.XadesNamespaceUri);

		var xmlNodeList = xmlElement.SelectNodes("xsd:Description", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			Description = xmlNodeList.Item(0).InnerText;
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:ObjectIdentifier", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			ObjectIdentifier = new ObjectIdentifier("ObjectIdentifier");
			ObjectIdentifier.LoadXml((XmlElement)xmlNodeList.Item(0));
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:MimeType", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			MimeType = xmlNodeList.Item(0).InnerText;
		}

		xmlNodeList = xmlElement.SelectNodes("xsd:Encoding", xmlNamespaceManager);
		if (xmlNodeList.Count != 0)
		{
			Encoding = xmlNodeList.Item(0).InnerText;
		}
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		XmlElement bufferXmlElement;
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "DataObjectFormat", XadesSignedXml.XadesNamespaceUri);

		if (!string.IsNullOrEmpty(ObjectReferenceAttribute))
		{
			retVal.SetAttribute("ObjectReference", ObjectReferenceAttribute);
		}
		else
		{
			throw new CryptographicException("Attribute ObjectReference missing");
		}

		if (!string.IsNullOrEmpty(Description))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "Description", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = Description;
			retVal.AppendChild(bufferXmlElement);
		}

		if (ObjectIdentifier != null && ObjectIdentifier.HasChanged())
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(ObjectIdentifier.GetXml(), true));
		}

		if (!string.IsNullOrEmpty(MimeType))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "MimeType", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = MimeType;
			retVal.AppendChild(bufferXmlElement);
		}

		if (!string.IsNullOrEmpty(Encoding))
		{
			bufferXmlElement = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "Encoding", XadesSignedXml.XadesNamespaceUri);
			bufferXmlElement.InnerText = Encoding;
			retVal.AppendChild(bufferXmlElement);
		}
		return retVal;
	}
}