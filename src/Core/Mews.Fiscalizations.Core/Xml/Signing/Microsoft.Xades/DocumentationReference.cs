using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// DocumentationReference points to further explanatory documentation
/// of the object identifier
/// </summary>
internal sealed class DocumentationReference
{
	/// <summary>
	/// Pointer to further explanatory documentation of the object identifier
	/// </summary>
	public string DocumentationReferenceUri { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public DocumentationReference()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(DocumentationReferenceUri);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        DocumentationReferenceUri = xmlElement.InnerText;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("DocumentationReference", XadesSignedXml.XadesNamespaceUri);
		retVal.InnerText = DocumentationReferenceUri;

		return retVal;
	}
}