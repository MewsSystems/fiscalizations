using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// Alternative forms of validation data can be included in this class
/// </summary>
internal sealed class OtherRef
{
	/// <summary>
	/// The generic XML element that represents any other type of ref
	/// </summary>
	public XmlElement AnyXmlElement { get; set; }

	/// <summary>
	/// Default constructor
	/// </summary>
	public OtherRef()
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
		var retVal = creationXmlDocument.CreateElement("OtherRef", XadesSignedXml.XadesNamespaceUri);

		if (AnyXmlElement != null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(AnyXmlElement, true));
		}

		return retVal;
	}
}