using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class contains properties that qualify some of the signed data
/// objects. The signature generated by the signer does not cover the content
/// of this element.
/// This information is added for the shake of completeness and to cope with
/// potential future needs for inclusion of such kind of properties.
/// </summary>
internal sealed class UnsignedDataObjectProperty
{
	/// <summary>
	/// The generic XML element that represents an unsigned data object
	/// </summary>
	public XmlElement AnyXmlElement { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public UnsignedDataObjectProperty()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public bool HasChanged()
	{
		return AnyXmlElement != null;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public void LoadXml(XmlElement xmlElement)
	{
		AnyXmlElement = xmlElement;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement("UnsignedDataObjectProperty", XadesSignedXml.XadesNamespaceUri);

		if (AnyXmlElement != null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(AnyXmlElement, true));
		}

		return retVal;
	}
}