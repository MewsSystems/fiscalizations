using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class refers to one ds:Reference element of the ds:SignedInfo
/// corresponding with one data object qualified by this property.
/// If some but not all the signed data objects share the same commitment,
/// one ObjectReference element must appear for each one of them.
/// However, if all the signed data objects share the same commitment,
/// the AllSignedDataObjects empty element must be present.
/// </summary>
internal sealed class ObjectReference
{
	/// <summary>
	/// Uri of the object reference
	/// </summary>
	public string ObjectReferenceUri { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public ObjectReference()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return !string.IsNullOrEmpty(ObjectReferenceUri);
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
        ArgumentNullException.ThrowIfNull(xmlElement);

        ObjectReferenceUri = xmlElement.InnerText;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("ObjectReference", XadesSignedXml.XadesNamespaceUri);
		retVal.InnerText = ObjectReferenceUri;

		return retVal;
	}
}