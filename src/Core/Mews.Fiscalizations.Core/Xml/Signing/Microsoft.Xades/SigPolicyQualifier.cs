using System.Xml;

namespace Mews.Fiscalizations.Core.Xml.Signing.Microsoft.Xades;

/// <summary>
/// This class can contain additional information qualifying the signature
/// policy identifier
/// </summary>
internal class SigPolicyQualifier
{
	private XmlElement anyXmlElement;

	/// <summary>
	/// The generic XML element that represents a sig policy qualifier
	/// </summary>
	public virtual XmlElement AnyXmlElement
	{
		get => anyXmlElement;
		set => anyXmlElement = value;
	}

	/// <summary>
	/// Default constructor
	/// </summary>
	public SigPolicyQualifier()
	{
	}

	/// <summary>
	/// Check to see if something has changed in this instance and needs to be serialized
	/// </summary>
	/// <returns>Flag indicating if a member needs serialization</returns>
	public virtual bool HasChanged()
	{
		return anyXmlElement is not null;
	}

	/// <summary>
	/// Load state from an XML element
	/// </summary>
	/// <param name="xmlElement">XML element containing new state</param>
	public virtual void LoadXml(XmlElement xmlElement)
	{
		anyXmlElement = xmlElement;
	}

	/// <summary>
	/// Returns the XML representation of the this object
	/// </summary>
	/// <returns>XML element containing the state of this object</returns>
	public virtual XmlElement GetXml()
	{
		var creationXmlDocument = new XmlDocument();
		var retVal = creationXmlDocument.CreateElement(XadesSignedXml.XmlXadesPrefix, "SigPolicyQualifier", XadesSignedXml.XadesNamespaceUri);

		if (anyXmlElement != null)
		{
			retVal.AppendChild(creationXmlDocument.ImportNode(anyXmlElement, true));
		}

		return retVal;
	}
}